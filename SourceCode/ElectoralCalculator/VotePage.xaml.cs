using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ElectoralCalculator
{
    /// <summary>
    /// Interaction logic for VotePage.xaml
    /// </summary>
    public partial class VotePage : Page
    {
        List<Model.SelectableCandidate> selectableCandidates = new List<Model.SelectableCandidate>();

        public VotePage()
        {
            InitializeComponent();

            LoadCandidatesData();
        }

        void LoadCandidatesData()
        {
            List<DataLayer.Candidate> candidates = BizzLayer.ElectionFacade.GetCandidates();
            foreach (DataLayer.Candidate candidate in candidates)
            {
                selectableCandidates.Add(new Model.SelectableCandidate() { candidate = candidate });
            }
            listBoxCandidates.ItemsSource = selectableCandidates;
        }

        public void Reset()
        {
            selectableCandidates.ForEach(x => x.IsSelected = false);
        }

        private void ListViewScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = scrollViewer;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void VoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.currentlyLoggedElectorate.voted == 1)
            {
                MessageBox.Show("You can vote only once", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var selectedCandidates = selectableCandidates.Where(c => c.IsSelected).ToList();
            //vote null when different than 1 candidates are selected
            //this is counted as non valid vote
            //confirmation windows
            string message = "";
            if(selectedCandidates.Count != 1)
            {
                message = "Your vote will be invalid.\nAre you sure you want cast an invalid vote?";
            }
            else
            {
                message = string.Format("You are voting for {0} from {1} party.\nAre you sure you want cast this vote?", 
                    selectedCandidates[0].candidate.name, 
                    selectedCandidates[0].candidate.party
                    );
            }

            MessageBoxResult result = MessageBox.Show(message, "Vote confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                BizzLayer.ElectionFacade.Vote(ref Globals.currentlyLoggedElectorate, selectedCandidates.Count != 1 ? null : selectedCandidates[0].candidate);
                ElectolarWindow.instance.SelectView(ElectolarWindow.ElectoralWindowView.Result);
            }
        }
    }
}
