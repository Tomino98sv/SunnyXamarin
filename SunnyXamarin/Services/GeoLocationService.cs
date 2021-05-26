using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;

namespace SunnyXamarin.Services
{
    public class GeoLocationService : IGeoLocationService
    {

		public async Task<Location> GetCurrentLocation()
		{
			Location userLocation = null;
			try
			{

				var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

				if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
				{
					userLocation = await CallGeoRequest();
					return userLocation;
				}
				else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
				{

					var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
					/*if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
					{
						//await DisplayAlert("Need location", "Gunna need that location", "OK");
					}*/
					Console.WriteLine("results ");

					if (results[Permission.Location] == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
					{
						userLocation = await CallGeoRequest();
						return userLocation;
					}

				}
				else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
				{
					//await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

			return userLocation;
		}

		private async Task<Location> CallGeoRequest()
		{

			Location userLocation = null;

			try
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
			catch (Exception e)
			{
				throw e;
			}

			return userLocation;


		}
	}
}
