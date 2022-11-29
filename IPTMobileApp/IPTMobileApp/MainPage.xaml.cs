using System;
using System.Timers;
using Xamarin.Forms;

namespace IPTMobileApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            AnimateCorousel();

        }
        Timer timer;

        private void AnimateCorousel()
        {
            timer = new Timer(5000) { AutoReset = true, Enabled = true };

            timer.Elapsed += (s, e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (cvOnboarding.Position == 2)
                    {
                        cvOnboarding.Position = 0;
                        return;
                    }

                    cvOnboarding.Position += 1;
                });
            };
        }

        private void SignUpBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        private void LoginBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}
