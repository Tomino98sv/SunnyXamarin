using System.ComponentModel;
using SunnyXamarin.Models;
using SunnyXamarin.Services;
using Xamarin.Forms;

namespace SunnyXamarin.ViewModels
{
    class WeatherCityViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Command CallWeatherReq { get; }

        private WeatherModel _weatherByCity = new WeatherModel();
        private string _cityZIP = string.Empty;
        private string _countryCode = string.Empty;
        private IOpenWeatherService _rest = DependencyService.Get<IOpenWeatherService>();


        public WeatherCityViewModel()
        {
            CallWeatherReq = new Command(async () =>
            {
                var result = await _rest.GetWeatherByCity(CityZIP, CountryCode);

                if (result != null)
                {
                    WeatherByCity = result;
                }
            });
        }

        public WeatherModel WeatherByCity
        {
            get => _weatherByCity;
            set 
            {
                if (_weatherByCity == value)
                {
                    return;
                }

                _weatherByCity = value;
                _weatherByCity.WeatherMain.Temp = _rest.Conversion_CelvinToCelzius(_weatherByCity.WeatherMain.Temp);
                _weatherByCity.WeatherMain.Temp_min = _rest.Conversion_CelvinToCelzius(_weatherByCity.WeatherMain.Temp_min);
                _weatherByCity.WeatherMain.Temp_max = _rest.Conversion_CelvinToCelzius(_weatherByCity.WeatherMain.Temp_max);
                _weatherByCity.WeatherMain.Feels_like = _rest.Conversion_CelvinToCelzius(_weatherByCity.WeatherMain.Feels_like);

                var args = new PropertyChangedEventArgs(nameof(WeatherByCity));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public string CityZIP
        {
            get => _cityZIP;
            set
            {
                if (_cityZIP == value)
                {
                    return;
                }

                _cityZIP = value;

                var args = new PropertyChangedEventArgs(nameof(CityZIP));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public string CountryCode
        {
            get => _countryCode;
            set
            {
                if (_countryCode == value)
                {
                    return;
                }

                _countryCode = value;

                var args = new PropertyChangedEventArgs(nameof(CountryCode));

                PropertyChanged?.Invoke(this, args);
            }
        }

    }
}
