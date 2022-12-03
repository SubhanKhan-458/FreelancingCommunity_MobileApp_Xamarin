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
    public partial class GigModalPage : ContentPage
    {
        JToken gig;
        public GigModalPage(JToken gig)
        {
            InitializeComponent();
            this.gig = gig;

            GigName.Text = gig["gigName"].ToString();
            GigDescription.Text = gig["description"].ToString();
            GigPay.Text = gig["pay"].ToString();
            GigDeadline.Date = (DateTime)gig["deadline"];
        }

        //Update Gig
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (GigName.Text == "")
                GigName.BackgroundColor = Color.LightPink;
            else if (GigDescription.Text == "")
                GigDescription.BackgroundColor = Color.LightPink;
            else if (GigPay.Text == "")
                GigPay.BackgroundColor = Color.LightPink;
            else
            {
                try
                {
                    var data = new StringContent(JsonConvert.SerializeObject(new
                    {
                        gigId = gig["gigId"].ToString(),
                        creatorId = LoginPage.loggedInUser["responseData"]["userId"].ToString(),
                        gigName = GigName.Text,
                        description = GigDescription.Text,
                        pay = Double.Parse(GigPay.Text),
                        deadline = GigDeadline.Date.ToString()
                    }), System.Text.Encoding.UTF8, "application/json");

                    Debug.WriteLine(data);

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
                    HttpResponseMessage responseMessage = await client.PutAsync("https://khudmadadbackendapi.azure-api.net/api/gig/update", data);
                    Debug.WriteLine(responseMessage);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Gig Updated");
                        await DisplayAlert("Success", "Gig Updated", "Ok");
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        Debug.WriteLine("Gig Not Updated");
                        await DisplayAlert("Failure", "Gig Not Updated", "Ok");
                        await Navigation.PopModalAsync();
                    }
                }
                catch
                {
                    Debug.WriteLine("Gig Not Updated");
                    await DisplayAlert("Failure", "Gig Not Updated", "Ok");
                    await Navigation.PopModalAsync();
                }
            }
        }

        //Delete Gig
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        gigId = gig["gigId"].ToString(),
                        creatorId = LoginPage.loggedInUser["responseData"]["userId"].ToString(),
                        gigName = GigName.Text,
                        description = GigDescription.Text,
                        pay = Double.Parse(GigPay.Text),
                        deadline = GigDeadline.Date.ToString()

                    }), Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri("https://khudmadadbackendapi.azure-api.net/api/gig/delete")
                };
                HttpResponseMessage responseMessage = await client.SendAsync(request);

                if (responseMessage.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Gig Deleted");
                    await DisplayAlert("Success", "Gig Deleted", "Ok");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    Debug.WriteLine("Gig Not Deleted");
                    await DisplayAlert("Failure", "Gig Not Deleted", "Ok");
                    await Navigation.PopModalAsync();
                }
            }
            catch
            {
                await Navigation.PopModalAsync();
            }
        }
    }
}