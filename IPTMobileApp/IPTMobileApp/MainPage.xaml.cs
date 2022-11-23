using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Timers;

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
