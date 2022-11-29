using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Diagnostics;

namespace IPTMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //Access with these names: Fname.Text, Lname.Text, Email.Text, Password.Text, CPassword.Text, Role.Text, CPhone_no.Text

            if(Fname.Text == "") 
                Fname.BackgroundColor = Color.LightPink;
            else if(Lname.Text == "") 
                Lname.BackgroundColor = Color.LightPink;
            else if(Email.Text == "") 
                Email.BackgroundColor = Color.LightPink;
            else if(Username.Text == "") 
                Username.BackgroundColor = Color.LightPink;
            else if(Password.Text == "") 
                Password.BackgroundColor = Color.LightPink;
            else if(CPassword.Text == "") 
                CPassword.BackgroundColor = Color.LightPink;
            else if(CPhone_no.Text == "")
                CPhone_no.BackgroundColor = Color.LightPink;
            else
            {
                dynamic data;
                try
                {
                    if (Role.SelectedItem == "Freelancer")
                    {
                        data = new StringContent(JsonConvert.SerializeObject(new {
                            firstName = Fname.Text,
                            lastName = Lname.Text,
                            //email = Email.Text,
                            username = Username.Text,
                            password = Password.Text,
                            dob = "09/03/2002",
                            phoneNumber = CPhone_no.Text,
                            roleId = 1
                        }), System.Text.Encoding.UTF8, "application/json");
                    }
                    else
                    {
                        data = new StringContent(JsonConvert.SerializeObject(new {
                            firstName = Fname.Text,
                            lastName = Lname.Text,
                            //email = Email.Text,
                            username = Username.Text,
                            password = Password.Text,
                            dob = "09/03/2002",
                            phoneNumber = CPhone_no.Text,
                            roleId = 2
                        }), System.Text.Encoding.UTF8, "application/json");
                    }
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
                    HttpResponseMessage responseMessage = await client.PostAsync("https://khudmadadbackendapi.azure-api.net/api/users/Create", data);
                    Debug.WriteLine(responseMessage);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Record Inserted");
                        await Navigation.PushAsync(new LoginPage());
                    }
                    else
                    {
                        Debug.WriteLine("Record Not Inserted");
                        await Navigation.PushAsync(new SignUpPage());
                    }
                }
                catch 
                {
                    await Navigation.PushAsync(new SignUpPage());
                }   

        }
    }
}