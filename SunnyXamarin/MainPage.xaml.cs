using Newtonsoft.Json;
using SunnyXamarin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;

namespace SunnyXamarin
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            SunnyXamarin.GoogleService.CreateAccountStore();
        }

        void OnGoogleLoginClick(object sender, EventArgs e)
        {
            SunnyXamarin.GoogleService.GoogleSignIn();
        }


       
    }
}
