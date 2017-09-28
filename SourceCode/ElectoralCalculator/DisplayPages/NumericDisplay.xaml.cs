using System.Collections.Generic;
using System.Windows.Controls;

namespace ElectoralCalculator.DisplayPages
{
    /// <summary>
    /// Interaction logic for NumericDisplay.xaml
    /// </summary>
    public partial class NumericDisplay : Page, IDataDisplayer
    {
        public double lastWindowWidth = 500;
        public double lastWindowHeight = 750;

        public bool needRecalculate = true;

        public NumericDisplay()
        {
            InitializeComponent();
        }

        public void DisplayData(Model.StatisticsData statisticData)
        {
            ElectolarWindow.instance.Width = lastWindowWidth;
            ElectolarWindow.instance.Height = lastWindowHeight;

            if (!needRecalculate)
            {
                return;
            }

            listBoxCandidates.ItemsSource = statisticData.candidateVotes;
            listBoxParties.ItemsSource = statisticData.partyVotes;
            var otherVotesStatistics = new Dictionary<string, int>();

            otherVotesStatistics.Add("Invalid votes", statisticData.invalidVotesNumber);

            listBoxOtherData.ItemsSource = otherVotesStatistics;

            needRecalculate = false;
        }
    }
}
