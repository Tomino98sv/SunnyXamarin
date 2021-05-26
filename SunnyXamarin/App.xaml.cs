using SunnyXamarin.Services;
using Xamarin.Forms;

namespace SunnyXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<IOpenWeatherService, OpenWeatherService>();
            DependencyService.Register<IGeoLocationService, GeoLocationService>();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
