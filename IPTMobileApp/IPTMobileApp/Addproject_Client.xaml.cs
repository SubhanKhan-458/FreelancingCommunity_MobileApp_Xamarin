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

        public Addproject_Client()
		{
			InitializeComponent ();
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
                    var data = new StringContent(JsonConvert.SerializeObject(new
                    {
                        creatorId = LoginPage.loggedInUser["responseData"]["userId"].ToString(),
                        gigName = GigName.Text,
                        description = Description.Text,
                        pay = Double.Parse(Pay.Text),
                        deadline = Deadline.Date.ToString()
                    }), System.Text.Encoding.UTF8, "application/json");

                    Debug.WriteLine(data);

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
                    HttpResponseMessage responseMessage = await client.PostAsync("https://khudmadadbackendapi.azure-api.net/api/gig/Create", data);
                    Debug.WriteLine(responseMessage);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Gig Inserted");
                        await Navigation.PushModalAsync(new LoggedinTabbedPage_Client());
                    }
                    else
                    {
                        Debug.WriteLine("Gig Not Inserted");
                        await Navigation.PushModalAsync(new LoggedinTabbedPage_Client());
                    }
                }
                catch
                {
                    await Navigation.PushModalAsync(new Addproject_Client()); //Idhar bhi tabbedpage ana chaihe
                }
            }
        }
    }
}