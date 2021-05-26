using SunnyXamarin.Models;
using System.Threading.Tasks;

namespace SunnyXamarin.Services
{
    interface IOpenWeatherService
    {
        Task<WeatherModel> GetWeatherByLocation(string longitude, string latitude);

        Task<WeatherModel> GetWeatherByCity(string cityZip, string countryCode);

        double Conversion_CelvinToCelzius(double celvin);

    }
}
