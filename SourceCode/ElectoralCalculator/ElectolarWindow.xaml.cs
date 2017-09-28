using System.Windows;

namespace ElectoralCalculator
{
    /// <summary>
    /// Interaction logic for ElectrolarWindow.xaml
    /// </summary>
    public partial class ElectolarWindow : Window
    {
        #region Singleton
        public static ElectolarWindow instance;
        #endregion

        VotePage votePage = new VotePage();
        ResultPage resultPage = new ResultPage();

        public ElectolarWindow()
        {
            instance = this;

            InitializeComponent();
            Closing += Globals.ShutDownApplication;
        }

        public void ShowWithReset()
        {
            SelectView(ElectoralWindowView.Vote);
            //loginText.DataContext = Globals.currentlyLoggedElectorate;
            votePage.Reset();
            Show();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LogOut();
        }

        public void LogOut()
        {
            BizzLayer.LoginFacade.LogOut(ref Globals.currentlyLoggedElectorate);
            Globals.currentlyLoggedElectorate = null;
            Globals.loginWindow.ShowWithReset();
            Hide();
        }

        public void SelectView(ElectoralWindowView view)
        {
            switch(view)
            {
                case ElectoralWindowView.Vote:
                    {
                        electoralWindowFrame.Content = votePage;
                    }
                    break;
                case ElectoralWindowView.Result:
                    {
                        electoralWindowFrame.Content = resultPage;
                        resultPage.Recalculate();
                    }
                    break;
            }
        }

        public enum ElectoralWindowView
        {
            Vote,
            Result
        }
    }
}
