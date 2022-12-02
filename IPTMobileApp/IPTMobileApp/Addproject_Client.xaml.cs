using Newtonsoft.Json;
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
	public partial class Addproject_Client : ContentPage
	{
        public JObject user;
		public Addproject_Client (JObject user)
		{
			InitializeComponent ();
            this.user = user;
            Debug.WriteLine(user["responseData"]["roleId"]);
		}

        public Addproject_Client ()
		{
			InitializeComponent ();
            this.user = LoginPage.loggedInUser;
            Debug.WriteLine(user["responseData"]["roleId"]);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (GigName.Text == "")
                GigName.BackgroundColor = Color.LightPink;
            else if (Description.Text == "")
                Description.BackgroundColor = Color.LightPink;
            else if (Pay.Text == "")
                Pay.BackgroundColor = Color.LightPink;
            else
            {
                try
                {
                    Debug.WriteLine(Deadline.Date.ToString());
                    var data = new StringContent(JsonConvert.SerializeObject(new
                    {
                        creatorId = user["responseData"]["userId"],
                        description = Description.Text,
                        pay = Int32.Parse(Pay.Text),
                        deadline = Deadline.Date.ToString()
                    }), System.Text.Encoding.UTF8, "application/json");

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
                    HttpResponseMessage responseMessage = await client.PostAsync("https://khudmadadbackendapi.azure-api.net/api/gig/Create", data);
                    Debug.WriteLine(responseMessage);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Gig Inserted");
                        await Navigation.PushAsync(new LoggedinPage_Client());
                    }
                    else
                    {
                        Debug.WriteLine("Gig Not Inserted");
                        await Navigation.PushAsync(new Addproject_Client());
                    }
                }
                catch
                {
                    await Navigation.PushAsync(new Addproject_Client());
                }
            }
        }
    }
}