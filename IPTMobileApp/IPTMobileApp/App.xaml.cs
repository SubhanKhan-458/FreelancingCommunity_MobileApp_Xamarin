using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPTMobileApp
{
    public partial class App : Application
    {
        public static string BaseURL;
        public static string EmailURL;
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] { "MediaElement_Experimental", "Brush_Experimental" });
            MainPage = new NavigationPage(new MainPage());
            BaseURL = "https://backend.khudmadad.co/";
            EmailURL = "https://5772-182-189-124-97.eu.ngrok.io/api/ConfirmationEmail";
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
