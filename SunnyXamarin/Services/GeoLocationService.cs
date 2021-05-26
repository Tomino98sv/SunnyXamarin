using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;

namespace SunnyXamarin.Services
{
    public class GeoLocationService : IGeoLocationService
    {

		private Location _userLocation;

		public async Task<Location> GetCurrentLocation()
		{
			try
			{

				var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

				if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
				{
					await CallGeoRequest();
					return _userLocation;
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
						_userLocation = await CallGeoRequest();
						return _userLocation;
					}

				}
				else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
				{
					var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
					
					if (results[Permission.Location] == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
					{
						_userLocation = await CallGeoRequest();
						return _userLocation;
					}
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

			return _userLocation;
		}

		private async Task<Location> CallGeoRequest()
		{

			try
			{
				var request = new GeolocationRequest(GeolocationAccuracy.Medium);
				_userLocation = await Geolocation.GetLocationAsync(request);
				if (_userLocation != null)
				{
					if (_userLocation.IsFromMockProvider)
					{
						// location is from a mock provider
						return _userLocation;
					}
					else
					{
						return _userLocation;
					}

				}
			}
			catch (Exception e)
			{
				throw e;
			}

			return _userLocation;

		}
	}
}
