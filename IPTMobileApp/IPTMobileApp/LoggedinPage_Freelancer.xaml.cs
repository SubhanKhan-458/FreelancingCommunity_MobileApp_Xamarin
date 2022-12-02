using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace IPTMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoggedinPage_Freelancer : ContentPage
    {
        public class Card
        {
            public string Img1 { get; set; }
            public string PName { get; set; }
            public string PDescription { set; get; }
            public string PCost { get; set; }
            public string PDeadline { get; set; }
        }

        JToken gigList;
        public ObservableCollection<Card> ListDetails { get; set; }

        public LoggedinPage_Freelancer()
        {
            InitializeComponent();

            this.PopulateGigs();

        }

        private async void PopulateGigs()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
            HttpResponseMessage httpResponse = await client.GetAsync("https://khudmadadbackend20221127230404.azurewebsites.net/api/gig");
            Debug.WriteLine(httpResponse);
            string response = await httpResponse.Content.ReadAsStringAsync();
            var res = JObject.Parse(response);
            Debug.WriteLine(res["responseData"]);
            gigList = res["responseData"];
            Debug.WriteLine(gigList.ToString());

            ListDetails = new ObservableCollection<Card>();
			for (int i = 0; i < gigList.Count(); i++)	//Like this make list and access each..
			{
                ListDetails.Add(new Card { Img1 = "freelancer", PName = "Medicare $olutions", PDescription = gigList[i]["description"].ToString(), PCost = gigList[i]["pay"].ToString(), PDeadline = gigList[i]["deadline"].ToString() });
			}	
            BindingContext = this;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var itemClicked = ((ListView)sender).SelectedItem;
            var index = ListDetails.IndexOf((Card)itemClicked);

            Debug.WriteLine(ListDetails[index].PDescription);
            bool decision = await DisplayAlert("Gig Offer", ListDetails[index].PDescription, "Accept", "Ignore");

            if (decision)
            {
                Debug.WriteLine(gigList[index]["gigId"].ToString());
                Debug.WriteLine(LoginPage.loggedInUser["responseData"]["userId"].ToString());
                Debug.WriteLine(gigList[index]["pay"].ToString());
                //Post Offer
                try
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(new
                    {
                        gigId = Int32.Parse(gigList[index]["gigId"].ToString()),
                        freelancerId = Int32.Parse(LoginPage.loggedInUser["responseData"]["userId"].ToString()),
                        pay = Double.Parse(gigList[index]["pay"].ToString())
                    }), Encoding.UTF8, "application/json");

                    Debug.WriteLine(data);

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
                    HttpResponseMessage responseMessage = await client.PostAsync("https://khudmadadbackend20221127230404.azurewebsites.net/api/offer/create", data);
                    Debug.WriteLine(responseMessage);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Offer Inserted");
                        await Navigation.PushAsync(new LoggedinPage_Freelancer());
                    }
                    else
                    {
                        Debug.WriteLine("Offer Not Inserted");
                        await Navigation.PushAsync(new LoggedinPage_Freelancer());
                    }
                }
                catch
                {
                    await Navigation.PushAsync(new LoggedinPage_Freelancer());
                }
            }

        }
    }
}