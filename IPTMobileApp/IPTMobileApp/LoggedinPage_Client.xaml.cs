using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPTMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoggedinPage_Client : ContentPage
    {
        public class Card
        {
            public string Img1 { get; set; }
            public string PName { get; set; }
            public string PDescription { set; get; }
            public string PCost { get; set; }
            public string PDeadline { get; set; }
            public string FreelancerEmail { get; set; }
            public string FreelancerName { get; set; }
            public string FreelancerDesc { get; set; }
            public string status { get; set; }
        }
        public ObservableCollection<Card> ListDetails { get; set; }
        JToken offerList;
        JObject user;

        public LoggedinPage_Client()
        {
            InitializeComponent();
            this.user = LoginPage.loggedInUser;
            this.PopulateOffers();
        }

        private async void PopulateOffers()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
            HttpResponseMessage httpResponse = await client.GetAsync("https://khudmadadbackend20221127230404.azurewebsites.net/api/offer/clientId/" + user["responseData"]["userId"]);
            Debug.WriteLine(httpResponse);
            string response = await httpResponse.Content.ReadAsStringAsync();
            var res = JObject.Parse(response);
            Debug.WriteLine(res["responseData"]);
            offerList = res["responseData"];

            ListDetails = new ObservableCollection<Card>();
            for (int i = 0; i < offerList.Count() ; i++) //Like this make list and access each..
            {
                ListDetails.Add(new Card { Img1 = "freelancer", PName = offerList[i]["gigName"].ToString(), PDescription = offerList[i]["gigDescription"].ToString(), PCost = offerList[i]["pay"].ToString(), PDeadline = offerList[i]["deadline"].ToString(), FreelancerName= offerList[i]["firstName"].ToString()  + " " + offerList[i]["lastName"].ToString(), FreelancerDesc= offerList[i]["freelancerDescription"].ToString(), FreelancerEmail= offerList[i]["email"].ToString(), status= offerList[i]["status"].ToString() });
            }
            BindingContext = this;
        } 

        private async void DeleteOffer(int index)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        gigId = Int32.Parse(offerList[index]["gigId"].ToString()),
                        freelancerId = Int32.Parse(offerList[index]["freelancerId"].ToString()),
                    }), Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri("https://khudmadadbackendapi.azure-api.net/api/Offer/delete")
                };
                HttpResponseMessage responseMessage = await client.SendAsync(request);
                Debug.WriteLine(responseMessage);

                if (responseMessage.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Offer Deleted");
                    await Navigation.PushAsync(new LoggedinPage_Client());
                }
                else
                {
                    Debug.WriteLine("Offer Delete Error");
                    await Navigation.PushAsync(new LoggedinPage_Client());
                }
            }
            catch
            {
                Debug.WriteLine("Offer Delete Error");
            }
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var itemClicked = ((ListView)sender).SelectedItem;
            var index = ListDetails.IndexOf((Card)itemClicked);

            Debug.WriteLine(index);
            Debug.WriteLine(ListDetails[index].PDescription);
            bool decision = await DisplayAlert("Gig Offer", ListDetails[index].FreelancerName, "Accept Offer", "Delete Offer");

            if (decision)
            {
                Debug.WriteLine(offerList[index]["gigId"].ToString());
                Debug.WriteLine(offerList[index]["freelancerId"].ToString());
                Debug.WriteLine(offerList[index]["pay"].ToString());
                //Post Offer
                try
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(new
                    {
                        gigId = Int32.Parse(offerList[index]["gigId"].ToString()),
                        freelancerId = Int32.Parse(offerList[index]["freelancerId"].ToString()),
                        pay = Double.Parse(offerList[index]["pay"].ToString()),
                        status = true
                    }), Encoding.UTF8, "application/json");

                    //Debug.WriteLine(data);

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
                    HttpResponseMessage responseMessage = await client.PutAsync("https://khudmadadbackend20221127230404.azurewebsites.net/api/offer/updatestatus", data);
                    HttpResponseMessage responseMessage1 = await client.DeleteAsync("https://khudmadadbackend20221127230404.azurewebsites.net/api/offer/delete/" + offerList[index]["gigId"].ToString());
                    //Debug.WriteLine(responseMessage);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Offer Updated");
                        ListDetails[index].status = true.ToString();
                        await Navigation.PushModalAsync(new LoggedinTabbedPage_Client());
                        try
                        {
                            //Email Freelancer regarding offer acceptance
                            data = new StringContent(JsonConvert.SerializeObject(new
                            {
                                email = offerList[index]["email"].ToString(),
                                gigId = Int32.Parse(offerList[index]["gigId"].ToString())
                            }), Encoding.UTF8, "application/json");

                            Debug.WriteLine(data.ToString());

                            var EmailClient = new HttpClient();
                            HttpResponseMessage emailMessage = await EmailClient.PostAsync("https://khudmadadfunction12345.azurewebsites.net/api/ConfirmationEmail?", data);
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                Debug.WriteLine("Email Sent");
                                await DisplayAlert("Success", "Confirmation Email Sent to Freelancer", "OK");
                            }
                            else
                            {
                                Debug.WriteLine("Error in Email");
                                await DisplayAlert("Failure", "Confirmation Email Not Sent to Freelancer", "OK");
                            }
                        }
                        catch
                        {
                            Debug.WriteLine("Error in Email");
                            await Navigation.PushAsync(new LoginPage());
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Offer Not Updated");
                        await Navigation.PushAsync(new LoggedinPage_Client());
                    }
                }
                catch
                {
                    await Navigation.PushAsync(new LoggedinPage_Client());
                }
            }
            else
            {
                bool opt = await DisplayAlert("Delete Offer", "Are you sure you want to delete this offer", "YES", "NO");
                if(opt)
                {
                    this.DeleteOffer(index);
                }
            }
        }

        private void Accept_Clicked(object sender, EventArgs e)
        {
            var itemClicked = ((ListView)sender).SelectedItem;
            var index = ListDetails.IndexOf((Card)itemClicked);
            
            Debug.WriteLine("Accept Button");
            Debug.WriteLine(index);
        }

        private void Reject_Clicked(object sender, EventArgs e)
        {
            var itemClicked = ((ListView)sender).SelectedItem;
            var index = ListDetails.IndexOf((Card)itemClicked);

            Debug.WriteLine("Reject Button");
            Debug.WriteLine(index);
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new LoginPage());
        }
    }
}