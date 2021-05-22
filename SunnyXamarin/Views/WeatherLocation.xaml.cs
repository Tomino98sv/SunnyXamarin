using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SunnyXamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WeatherLocation : ContentPage
	{
		public WeatherLocation ()
		{
			InitializeComponent ();
		}

		private async void CheckInButton_Clicked(object sender, EventArgs e)
		{
			await Task.Run(CheckIn);
		}

		private async Task CheckIn()
		{
			try
			{

				var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

				if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
				{
					var request = new GeolocationRequest(GeolocationAccuracy.Medium);
					var userLocation = await Geolocation.GetLocationAsync(request);
					if (userLocation != null)
					{
						if (userLocation.IsFromMockProvider)
						{
							// location is from a mock provider
							lblLat.Text = "Longitude : " + userLocation.Longitude;
							lblLon.Text = "Latitude : " + userLocation.Latitude;
						}
						else
						{
							lblLat.Text = "Longitude : " + userLocation.Longitude;
							lblLon.Text = "Latitude : " + userLocation.Latitude;
						}

					}
				}
				else if(status != Plugin.Permissions.Abstractions.PermissionStatus.Granted) 
				{
					if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
					{
						await DisplayAlert("Need location", "Gunna need that location", "OK");
					}

					var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
					status = results[Permission.Location];
				}
				else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
				{
					await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
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
		}
	}
}