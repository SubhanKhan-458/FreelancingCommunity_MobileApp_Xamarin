using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPTMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoggedinPage_Freelancer : ContentPage
    {
        public LoggedinPage_Freelancer()
        {
            InitializeComponent();
            displayCardBox();
        }
        private void displayCardBox()
        {
            for (int i = 0; i < 2; i++)
            {

            }
        }
    }
}