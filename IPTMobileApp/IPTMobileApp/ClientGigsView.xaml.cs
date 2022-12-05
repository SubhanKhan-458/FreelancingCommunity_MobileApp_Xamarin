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
    public partial class ClientGigsView : ContentPage
    {
        public class Card
        {
            public string Img1 { get; set; }
            public string PName { get; set; }
            public string PDescription { set; get; }
            public string PCost { get; set; }
            public string PDeadline { get; set; }
        }

        JObject user;
        JToken gigList;
        public ObservableCollection<Card> ListDetails { get; set; }

        public ClientGigsView()
        {
            InitializeComponent();
            this.user = LoginPage.loggedInUser;
            this.PopulateGigs();
        }

        private async void PopulateGigs()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
            HttpResponseMessage httpResponse = await client.GetAsync("https://khudmadadbackend20221127230404.azurewebsites.net/api/gig/creatorId/" + user["responseData"]["userId"].ToString());
            Debug.WriteLine(httpResponse);
            string response = await httpResponse.Content.ReadAsStringAsync();
            var res = JObject.Parse(response);
            Debug.WriteLine(res["responseData"]);
            gigList = res["responseData"];  
            Debug.WriteLine(gigList.ToString());

            ListDetails = new ObservableCollection<Card>();
            for (int i = 0; i < gigList.Count(); i++)   //Like this make list and access each..
            {
                ListDetails.Add(new Card { Img1 = "freelancer", PName = gigList[i]["gigName"].ToString(), PDescription = gigList[i]["description"].ToString(), PCost = gigList[i]["pay"].ToString(), PDeadline = gigList[i]["deadline"].ToString() });
            }
            BindingContext = this;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var itemClicked = ((ListView)sender).SelectedItem;
            var index = ListDetails.IndexOf((Card)itemClicked);
            await Navigation.PushModalAsync(new GigModalPage(this.gigList[index]));
        }
    }
}