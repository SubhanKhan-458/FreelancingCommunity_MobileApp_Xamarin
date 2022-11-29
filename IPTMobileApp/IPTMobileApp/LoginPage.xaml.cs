﻿using System;
using System.Net.Http.Json;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace IPTMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
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
                try
                {
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
                    HttpResponseMessage httpResponse = await client.GetAsync("https://khudmadadbackendapi.azure-api.net/api/users/userName/" + usernameEntry.Text);
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
                        if (res["responseData"]["roleId"].ToString() == "1")
                            await Navigation.PushAsync(new LoggedinPage_Freelancer());
                        else if (res["responseData"]["roleId"].ToString() == "2")
                            await Navigation.PushAsync(new LoggedinPage_Client());
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