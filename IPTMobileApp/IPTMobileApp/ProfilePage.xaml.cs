using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPTMobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		private JObject user; 
        public string fname { get; set; }
        public string lname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phoneNo { get; set; }
        public string desc { get; set; }

		public ProfilePage ()
		{
			InitializeComponent ();
			this.user = LoginPage.loggedInUser;
            Debug.WriteLine(user["responseData"]["userId"]);
            fname = user["responseData"]["firstName"].ToString();
            lname = user["responseData"]["lastName"].ToString();
            email = user["responseData"]["email"].ToString();
            username = user["responseData"]["userName"].ToString();
            password = user["responseData"]["password"].ToString();
            phoneNo = user["responseData"]["phoneNumber"].ToString();
            desc = user["responseData"]["description"].ToString();
            BindingContext = this;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Fname.Text == "")
                Fname.BackgroundColor = Color.LightPink;
            else if (Lname.Text == "")
                Lname.BackgroundColor = Color.LightPink;
            else if (Email.Text == "")
                Email.BackgroundColor = Color.LightPink;
            else if (PhoneNo.Text == "")
                PhoneNo.BackgroundColor = Color.LightPink;
            else
            {      
                try
                {
                    StringContent data = new StringContent(JsonConvert.SerializeObject(new
                    {
                        userId = Int32.Parse(user["responseData"]["userId"].ToString()),
                        roleId = Int32.Parse(user["responseData"]["roleId"].ToString()),
                        firstName = this.fname,
                        lastName = this.lname,
                        email = this.email,
                        username = Username.Text,
                        password = Password.Text,
                        dob = user["responseData"]["dob"].ToString(),
                        phoneNumber = this.phoneNo,
                        description = this.desc,
                    }), Encoding.UTF8, "application/json");

                    Debug.WriteLine(data);

                    var client = new HttpClient();
                    //client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cd53344c9a0e4abeb55ea6322888d4d6");
                    HttpResponseMessage responseMessage = await client.PutAsync(App.BaseURL + "api/Users/update", data);
                    Debug.WriteLine(responseMessage);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Record Updated");
                        await DisplayAlert("Success", "Record Updated", "Ok");
                    }
                    else
                    {
                        Debug.WriteLine("Record Not Updated");
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