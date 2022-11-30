using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPTMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] { "MediaElement_Experimental", "Brush_Experimental" });
            MainPage = new NavigationPage(new LoggedinPage_Client());
            
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
