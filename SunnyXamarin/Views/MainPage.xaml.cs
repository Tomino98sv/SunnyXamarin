using SunnyXamarin.Services;
using System;
using Xamarin.Forms;

namespace SunnyXamarin
{
    public partial class MainPage : ContentPage
    {
        private IGoogleService _googleAuth = DependencyService.Get<IGoogleService>();

        public MainPage()
        {
            InitializeComponent();

            _googleAuth.GetAccountStore();
        }

        void OnGoogleLoginClick(object sender, EventArgs e)
        {
            _googleAuth.GoogleSignIn();
        }


       
    }
}
