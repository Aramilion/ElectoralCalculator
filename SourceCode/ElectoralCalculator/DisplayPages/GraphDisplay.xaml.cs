using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ElectoralCalculator.DisplayPages
{
    /// <summary>
    /// Interaction logic for GraphDisplay.xaml
    /// </summary>
    public partial class GraphDisplay : Page, IDataDisplayer
    {
        public bool needRecalculate = true;
        private const int desiredWindowWidth = 1000;
        private const int desiredWindowHeight = 750;

        public GraphDisplay()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public void DisplayData(Model.StatisticsData statisticData)
        {
            ElectolarWindow.instance.Width = desiredWindowWidth;
            ElectolarWindow.instance.Height = desiredWindowHeight;

            if (!needRecalculate)
            {
                return;
            }

            //BarChart.ItemsSource = null;
            ChartDataCollection.Clear();
            BarChart.Legends.Clear();
            foreach (var candidateVotes in statisticData.candidateVotes)
            {
                ChartDataCollection.Add(new ChartData() { X = 1, Value = candidateVotes.Value, EntryName = candidateVotes.Key.name });
            }


            foreach (var partyVotes in statisticData.partyVotes)
            {
                ChartDataCollection.Add(new ChartData() { X = 2, Value = partyVotes.Value, EntryName = partyVotes.Key });
            }

            ChartDataCollection.Add(new ChartData() { X = 3, Value = statisticData.invalidVotesNumber, EntryName = "Invalid votes" });

            BarChart.ItemsSource = ChartDataCollection;

            needRecalculate = false;
        }

    #region ChartData
        private ObservableCollection<ChartData> chartDataCollection = new ObservableCollection<ChartData>();
        public ObservableCollection<ChartData> ChartDataCollection
        {
            get
            {
                return chartDataCollection;
            }
            set
            {
                chartDataCollection = value;
                Notify("ChartDataCollection");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    #endregion

        private void Notify(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }

    public class ChartData
    {
        public ChartData()
        {
        }

        public double X { get; set; }

        public double Value { get; set; }

        public string EntryName { get; set; }
    }

    #region Converter
    public class Bool2Visibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
                return (Visibility)value == Visibility.Visible ? true : false;
            else
                throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            else
                throw new NotImplementedException();
        }
    }
    #endregion Converter
}
