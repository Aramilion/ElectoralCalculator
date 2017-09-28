namespace ElectoralCalculator
{
    public static class Globals
    {
        public static DataLayer.Electorate currentlyLoggedElectorate;
        public static LoginWindow loginWindow;
        public static ElectolarWindow electoralWindow = new ElectolarWindow();

        public static void ShutDownApplication(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
