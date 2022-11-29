using System.Collections.Generic;

namespace IPTMobileApp
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Onboardings = GetOnboarding();
        }

        public List<Onboarding> Onboardings { get; set; }
        private List<Onboarding> GetOnboarding()
        {
            return new List<Onboarding>
        {
            new Onboarding { Heading = "Mohnish Pabrai", Caption = "Entrepreneurs Are Great At Dealing With Uncertainty And Also Very Good At Minimizing Risk. That’s The Classic Entrepreneur." },
            new Onboarding { Heading = "Steve Jobs", Caption = "If You Are Working On Something That You Really Care About, You Don’t Have To Be Pushed. The Vision Pulls You." },
            new Onboarding { Heading = "Paulo Coelho, The Alchemist", Caption = "There is only one thing that makes a dream impossible to achieve: the fear of failure." }
        };
        }
    }
    public class Onboarding
    {
        public string Heading { get; set; }
        public string Caption { get; set; }
    }
}




