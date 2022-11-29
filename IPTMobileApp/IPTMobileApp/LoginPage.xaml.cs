using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPTMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(usernameEntry.Text=="Freelancer" && passwordEntry.Text=="Freelancer")
                Navigation.PushAsync(new LoggedinTabbedPage_Freelancer());
            else if(usernameEntry.Text == "Client" && passwordEntry.Text == "Client")
                Navigation.PushAsync(new LoggedinTabbedPage_Client());
            else
                Navigation.PushAsync(new LoginPage());
        }
    }
}