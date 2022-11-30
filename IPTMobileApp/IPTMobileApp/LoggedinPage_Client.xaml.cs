using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPTMobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoggedinPage_Client : ContentPage
	{
		public ObservableCollection<Card> ListDetails { get; set; }
		public LoggedinPage_Client()
		{
			InitializeComponent();
			ListDetails = new ObservableCollection<Card>
			{
				/*for (int i = 0; i < 2; i++)	Like this make list and access each..
				{
					new Card { Img1 = "freelancer", PName = PName[i], PDescription = "Cross platform app for medicines supply..", PCost = "30000", PDeadline = "22/11/2022" },
				}*/	
				new Card{Img1="freelancer", PName="Medicare $olutions", PDescription="Cross platform app for medicines supply..", PCost="30000", PDeadline="22/11/2022"},
                new Card{Img1="freelancer", PName="Textile automator", PDescription="Machine learning model for determining flaws in fabric..", PCost="30000", PDeadline="22/11/2022"}
            };
			BindingContext = this;
		}
	}
	public class Card
	{
		public string Img1 { get; set; }
		public string PName { get; set; }
		public string PDescription { set; get; }
		public string PCost { get; set; }
		public string PDeadline { get; set; }
	}
}