using System.ComponentModel;
using SunnyXamarin.Models;
using SunnyXamarin.Services;
using Xamarin.Forms;

namespace SunnyXamarin.ViewModels
{
    class WeatherCityViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private WeatherModel weatherByCity = new WeatherModel();
        private string cityZIP = string.Empty;
        private string countryCode = string.Empty;

        IOpenWeatherService _rest = DependencyService.Get<IOpenWeatherService>();

        public WeatherModel WeatherByCity
        {
            get => weatherByCity;
            set 
            {
                if (weatherByCity == value)
                {
                    return;
                }

                weatherByCity = value;

                var args = new PropertyChangedEventArgs(nameof(WeatherByCity));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public string CityZIP
        {
            get => cityZIP;
            set
            {
                if (cityZIP == value)
                {
                    return;
                }

                cityZIP = value;

                var args = new PropertyChangedEventArgs(nameof(CityZIP));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public string CountryCode
        {
            get => countryCode;
            set
            {
                if (countryCode == value)
                {
                    return;
                }

                countryCode = value;

                var args = new PropertyChangedEventArgs(nameof(CountryCode));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command CallWeatherReq { get; }
        

        public WeatherCityViewModel()
        {
            CallWeatherReq = new Command(async () =>
            {
                var result = await _rest.getWeatherByCity(CityZIP, CountryCode);

                if (result != null)
                {
                    WeatherByCity = result;
                }
            });
        }
    }
}
