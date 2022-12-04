using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPTMobileApp
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public static JObject loggedInUser;
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (usernameEntry.Text == "")
            {
                usernameEntry.BackgroundColor = Color.LightPink;
            }
            else if (passwordEntry.Text == "")
            {
                passwordEntry.BackgroundColor = Color.LightPink;
            }
            else
            {
                Debug.WriteLine(usernameEntry.Text);
                try
                {
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
                    HttpResponseMessage httpResponse = await client.GetAsync("https://khudmadadbackendapi.azure-api.net/api/users/userName/" + usernameEntry.Text);
                    Debug.WriteLine(httpResponse);
                    string response = await httpResponse.Content.ReadAsStringAsync();
                    var res = JObject.Parse(response);
                    Debug.WriteLine(res["responseData"]);

                    if (res["responseData"] == null)
                    {
                        usernameEntry.BackgroundColor = Color.LightPink;
                    }
                    else if (res["responseData"]["password"].ToString() != passwordEntry.Text)
                    {
                        passwordEntry.BackgroundColor = Color.LightPink;
                    }
                    else
                    {
                        loggedInUser = res;
                        if (res["responseData"]["roleId"].ToString() == "1")
                        {
                            await Navigation.PushModalAsync(new LoggedinTabbedPage_Freelancer());
                        }
                        else if (res["responseData"]["roleId"].ToString() == "2")
                        {
                            await Navigation.PushModalAsync(new LoggedinTabbedPage_Client());
                        }

                    }
                }
                catch
                {
                    await Navigation.PushAsync(new LoginPage());
                }
            }
        }
    }
}