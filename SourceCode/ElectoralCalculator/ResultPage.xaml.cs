using System;
using System.Windows;
using System.Windows.Controls;
using ElectoralCalculator.DisplayPages;
using System.ComponentModel;
using System.Drawing;

namespace ElectoralCalculator
{
    /// <summary>
    /// Interaction logic for ResultPage.xaml
    /// </summary>
    public partial class ResultPage : Page, INotifyPropertyChanged
    {
        GraphDisplay graphDisplay = new GraphDisplay();
        NumericDisplay numericDisplay = new NumericDisplay();

        ResultFrameView currentResultView;

        Model.StatisticsData statisticsData;

        #region View

        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsNumericDisplay
        {
            get
            {
                return currentResultView == ResultFrameView.Numeric;
            }
        }
        public bool IsGraphDisplay
        {
            get
            {
                return currentResultView == ResultFrameView.Graph;
            }
        }

        //public Brush ForegroundNumericButtonColor
        //{
        //    get
        //    {
        //        return currentResultView == ResultFrameView.Numeric ? Brushes.Black : Brushes.White;
        //    }
        //}
        //
        //public Brush ForegroundGraphButtonColor
        //{
        //    get
        //    {
        //        return currentResultView == ResultFrameView.Graph ? new SolidBrush(Color.White) : new SolidBrush(Color.Black);
        //    }
        //}
        #endregion

        public ResultPage()
        {
            InitializeComponent();

            exportComboBox.ItemsSource = Enum.GetValues(typeof(ExportFormat));
            exportComboBox.SelectedIndex = 0;
            DataContext = this;
        }

        void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void Recalculate()
        {
            statisticsData = PrepareData();
            numericDisplay.needRecalculate = true;
            graphDisplay.needRecalculate = true;
            numericDisplay.lastWindowWidth = ElectolarWindow.instance.Width;
            numericDisplay.lastWindowHeight = ElectolarWindow.instance.Height;
            exportComboBox.SelectedIndex = 0;
            SelectView(ResultFrameView.Numeric);
        }

        public void SelectView(ResultFrameView view)
        {        
            IDataDisplayer display = null;
            switch (view)
            {
                case ResultFrameView.Numeric:
                    {
                        ElectolarWindow.instance.Width = numericDisplay.lastWindowWidth;
                        ElectolarWindow.instance.Height = numericDisplay.lastWindowHeight;
                        display = numericDisplay;
                    }
                    break;
                case ResultFrameView.Graph:
                    {
                        numericDisplay.lastWindowWidth = ElectolarWindow.instance.Width;
                        numericDisplay.lastWindowHeight = ElectolarWindow.instance.Height;
                        display = graphDisplay;
                    }
                    break;
            }
            resultDisplayFrame.Content = display;

            display.DisplayData(statisticsData);

            currentResultView = view;
            NotifyPropertyChanged("IsNumericDisplay");
            NotifyPropertyChanged("IsGraphDisplay");
            //NotifyPropertyChanged("ForegroundNumericButtonColor");
            //NotifyPropertyChanged("ForegroundGraphButtonColor");
        }

        Model.StatisticsData PrepareData()
        {
            int votesNumber = BizzLayer.VoteAnalysisFacade.GetVotesNumber(valid: true);
            int invalidVotesNumber = BizzLayer.VoteAnalysisFacade.GetVotesNumber(valid: false);
            int withoutRightsVotesNumber = BizzLayer.VoteAnalysisFacade.GetVotesWithoutRightsNumber();
            var candidateVotes = BizzLayer.VoteAnalysisFacade.GetNumberOfVotesForCandidates();
            var partyVotes = BizzLayer.VoteAnalysisFacade.GetNumberOfVotesForParties();
            var rawVotesData = BizzLayer.VoteAnalysisFacade.GetVotesAllData();

            statisticsData = new Model.StatisticsData()
            {
                rawData = rawVotesData,
                validVotesNumber = votesNumber,
                invalidVotesNumber = invalidVotesNumber,
                withoutRightsVotesNumber = withoutRightsVotesNumber,
                candidateVotes = candidateVotes,
                partyVotes = partyVotes,      
            };

            return statisticsData;
        }

        public enum ResultFrameView
        {
            Numeric,
            Graph
        }

        private void ChangeViewButton_Click(object sender, RoutedEventArgs e)
        {
            SelectView(currentResultView == ResultFrameView.Numeric ? ResultFrameView.Graph : ResultFrameView.Numeric);
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();

            ParsingUtility.IDataExporter dataExporter = null;
            switch (exportComboBox.SelectedItem.ToString())
            {
                case "PDF":
                    {
                        saveFileDialog.DefaultExt = ".pdf";
                        saveFileDialog.Filter = "Text documents (.pdf)|*.pdf";
                        dataExporter = new ParsingUtility.PdfCreator();
                    }
                    break;
                case "CSV":
                    {
                        saveFileDialog.DefaultExt = ".csv";
                        saveFileDialog.Filter = "Text documents (.csv)|*.csv";
                        dataExporter = new ParsingUtility.CsvCreator();
                    }
                    break;
                default:
                    {
                        MessageBox.Show("Choose file type to export to.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
            }
 
            saveFileDialog.FileName = "statisticsData";
            
            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                ExportStatisticData(saveFileDialog.FileName, dataExporter, statisticsData, true);
            }
        }

        public void ExportStatisticData(string fileName, ParsingUtility.IDataExporter dataExporter, Model.StatisticsData statisticData, bool openFileAfterFinish = false)
        {
            try
            {
                if(System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }
                dataExporter.ExportData(fileName, statisticsData);
                if (openFileAfterFinish)
                {
                    System.Diagnostics.Process.Start(fileName);
                }
            }
            catch(System.IO.IOException)
            {
                MessageBox.Show("File is in use. Please close it or choose different path.", "Can't write file!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("File is in use. Please close it or choose different path.", "Can't write file!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public enum ExportFormat
        {
            PDF,
            CSV
        }
    }
}
