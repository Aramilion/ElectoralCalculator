using DataLayer;
using System.Windows;

namespace ElectoralCalculator
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            Globals.loginWindow = this;
            Closing += Globals.ShutDownApplication;
        }

        public void ShowWithReset()
        {
            nameTexBox.Text = "";
            surnameTexBox.Text = "";
            peselTexBox.Text = "";
            Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(nameTexBox.Text == string.Empty || surnameTexBox.Text == string.Empty || peselTexBox.Text == string.Empty)
            {
                BizzLayer.LoginFacade.RegisterLoginAttemp(peselTexBox.Text, succesful: false, valid: false);
                MessageBox.Show("Wrong login data", "Login problem", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string name = nameTexBox.Text;
            string surname = surnameTexBox.Text;
            string pesel = peselTexBox.Text;
            Model.PeselData peselData = ParsingUtility.PeselParser.GetPeselData(pesel);

            //check if pesel is valid
            if (peselData == null)
            {
                BizzLayer.LoginFacade.RegisterLoginAttemp(null, succesful: false, valid: false);
                MessageBox.Show("Invalid Pesel!", "Login problem", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //check if user exists in database
            Electorate templateElectorate = new Electorate()
            {
                name = ParsingUtility.HashCreator.GetHash(name),
                surname = ParsingUtility.HashCreator.GetHash(surname),
                pesel = peselData.Pesel
            };
            Electorate electorate = BizzLayer.ElectorateFacade.GetElectorate(templateElectorate);
            if (electorate == null)
            {
                bool? gotResult = BizzLayer.LoginFacade.CheckVotingRights(peselData);
                if (gotResult == false)
                {
                    BizzLayer.LoginFacade.RegisterAttempToVoteWitoutRights();
                    BizzLayer.LoginFacade.RegisterLoginAttemp(peselData.Pesel, succesful: false, valid: true);
                    MessageBox.Show("I am sorry, you have no voting rights.", "Login problem", MessageBoxButton.OK, MessageBoxImage.Information);    
                    return;
                }
                else if(gotResult == null)
                {
                    MessageBox.Show("Cannot connect to fetch some login data.\nPlease check your internet connection.", "Login problem", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                
                electorate = BizzLayer.ElectorateFacade.AddNewElectorate(templateElectorate);
                if(electorate == null)
                {
                    BizzLayer.LoginFacade.RegisterLoginAttemp(peselData.Pesel, succesful: false, valid: false);
                    MessageBox.Show("Wrong login data", "Login problem", MessageBoxButton.OK, MessageBoxImage.Error);                   
                    return;
                }
            }

            if(BizzLayer.LoginFacade.TryLogin(ref electorate))
            {
                if(electorate.voted == 1)
                {
                    BizzLayer.LoginFacade.RegisterLoginAttemp(peselData.Pesel, succesful: false, valid: true);
                    MessageBox.Show("You can not vote more than once", "Login problem", MessageBoxButton.OK, MessageBoxImage.Information);     
                    return;
                }
                Globals.currentlyLoggedElectorate = electorate;
                Globals.electoralWindow.ShowWithReset();
                Hide();
                BizzLayer.LoginFacade.RegisterLoginAttemp(peselData.Pesel, succesful: true, valid: true);
            }
            else
            {
                BizzLayer.LoginFacade.RegisterLoginAttemp(peselData.Pesel, succesful: false, valid: true);
                MessageBox.Show("Someone is already logged on this account", "Login problem", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }
    }
}
