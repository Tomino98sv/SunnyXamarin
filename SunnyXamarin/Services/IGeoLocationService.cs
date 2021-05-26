using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SunnyXamarin.Services
{
    interface IGeoLocationService
    {
        Task<Location> GetCurrentLocation();
    }
}
