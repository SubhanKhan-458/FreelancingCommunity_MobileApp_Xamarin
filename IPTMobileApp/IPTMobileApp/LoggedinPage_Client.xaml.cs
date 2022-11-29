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
		public LoggedinPage_Client ()
		{
			InitializeComponent ();
			ListDetails = new ObservableCollection<Card>
			{
				new Card{Img1="freelancer", Name="Subhan"}
			};
			BindingContext = this;
		}
	}
	public class Card
	{
		public string Img1 { get; set; }
		public string Name { get; set; }
	}
}