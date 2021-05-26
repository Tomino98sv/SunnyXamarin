using System;
using Xamarin.Forms;

namespace SunnyXamarin
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            SunnyXamarin.GoogleService.GetAccountStore();
        }

        void OnGoogleLoginClick(object sender, EventArgs e)
        {
            SunnyXamarin.GoogleService.GoogleSignIn();
        }


       
    }
}
