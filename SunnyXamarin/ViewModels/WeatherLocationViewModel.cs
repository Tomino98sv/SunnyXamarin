using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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

        private WeatherModel weatherByLoc = new WeatherModel();

        IOpenWeatherService _rest = DependencyService.Get<IOpenWeatherService>();
        public WeatherModel WeatherByLoc
        {
            get => weatherByLoc;
            set
            {
                if (weatherByLoc == value)
                {
                    return;
                }

                weatherByLoc = value;

                var args = new PropertyChangedEventArgs(nameof(WeatherByLoc));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command CallWeatherReq { get; }

		private async Task<Location> GetLocation()
		{
			Location userLocation = null;
			try
			{

				var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

				if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
				{
					var request = new GeolocationRequest(GeolocationAccuracy.Medium);
					userLocation = await Geolocation.GetLocationAsync(request);
					if (userLocation != null)
					{
						if (userLocation.IsFromMockProvider)
						{
							// location is from a mock provider
							return userLocation;
						}
						else
						{
							return userLocation;
						}

					}
				}
				else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
				{
					if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
					{
						//await DisplayAlert("Need location", "Gunna need that location", "OK");
					}

					var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
					status = results[Permission.Location];
				}
				else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
				{
					//await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
				}

			}
			catch (FeatureNotSupportedException fnsEx)
			{
				// Handle not supported on device exception
				//await Device.InvokeOnMainThreadAsync(() => MessageLabel.Text = "The check -in feature is not supported on your device.");
			}
			catch (FeatureNotEnabledException fneEx)
			{
				// Handle not enabled on device exception
				//await Device.InvokeOnMainThreadAsync(() => MessageLabel.Text = "The check-in feature is not enabled on your device.");
			}
			catch (PermissionException pEx)
			{
				// Handle permission exception
				//await Device.InvokeOnMainThreadAsync(() => MessageLabel.Text = "You need to grant location permission for the check-in feature to work on your device.");

			}
			catch (Exception ex)
			{
				// Unable to get location
				//await Device.InvokeOnMainThreadAsync(() => MessageLabel.Text = "We are unable to retrieve your location.");
			}

			return userLocation;
		}

		public WeatherLocationViewModel()
        {
            CallWeatherReq = new Command(async () =>
            {
				Location location = await GetLocation();                
				var result = await _rest.getWeatherByLocation(location.Longitude.ToString(), location.Latitude.ToString());

                if (result != null)
                {
                    WeatherByLoc = result;
                }
            });
        }
    }
}
