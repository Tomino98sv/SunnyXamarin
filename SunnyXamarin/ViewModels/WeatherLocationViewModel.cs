using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SunnyXamarin.Models;
using SunnyXamarin.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SunnyXamarin.ViewModels
{
    public class WeatherLocationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public Command CallWeatherReq { get; }


		private WeatherModel _weatherByLoc = new WeatherModel();
        private IOpenWeatherService _rest = DependencyService.Get<IOpenWeatherService>();
        private IGeoLocationService _geo = DependencyService.Get<IGeoLocationService>();

		public WeatherLocationViewModel()
		{
			CallWeatherReq = new Command(async () =>
			{
				Location location = await _geo.GetCurrentLocation();
				var result = await _rest.getWeatherByLocation(location.Longitude.ToString(), location.Latitude.ToString());

				if (result != null)
				{
					WeatherByLoc = result;
				}
			});
		}


		public WeatherModel WeatherByLoc
        {
            get => _weatherByLoc;
            set
            {
                if (_weatherByLoc == value)
                {
                    return;
                }

				_weatherByLoc = value;
				_weatherByLoc.WeatherMain.temp = _rest.Conversion_CelvinToCelzius(_weatherByLoc.WeatherMain.temp);
				_weatherByLoc.WeatherMain.temp_min = _rest.Conversion_CelvinToCelzius(_weatherByLoc.WeatherMain.temp_min);
				_weatherByLoc.WeatherMain.temp_max = _rest.Conversion_CelvinToCelzius(_weatherByLoc.WeatherMain.temp_max);
				_weatherByLoc.WeatherMain.feels_like = _rest.Conversion_CelvinToCelzius(_weatherByLoc.WeatherMain.feels_like);

                var args = new PropertyChangedEventArgs(nameof(WeatherByLoc));

                PropertyChanged?.Invoke(this, args);
            }
        }

	}
}
