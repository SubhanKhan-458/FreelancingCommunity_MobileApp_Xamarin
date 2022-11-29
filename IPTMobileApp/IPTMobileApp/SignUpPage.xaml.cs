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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Access with these names: Fname.Text, Lname.Text, Email.Text, Password.Text, CPassword.Text, Role.Text, CPhone_no.Text
            if(Fname.Text == "subhan")
            {
                Fname.Text = "";    
            }
        }
    }
}