namespace IPTMobileApp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new IPTMobileApp.App());
        }
    }
}
