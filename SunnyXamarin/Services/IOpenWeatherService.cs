using SunnyXamarin.Models;
using System.Threading.Tasks;

namespace SunnyXamarin.Services
{
    interface IOpenWeatherService
    {
        Task<WeatherModel> getWeatherByLocation(string longitude, string latitude);

        Task<WeatherModel> getWeatherByCity(string cityZip, string countryCode);

        double Conversion_CelvinToCelzius(double celvin);

    }
}
