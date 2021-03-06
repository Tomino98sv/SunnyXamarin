using SunnyXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SunnyXamarin.Services
{
    interface IOpenWeatherService
    {
        Task<WeatherModel> getWeatherByLocation(string longitude, string latitude);

        Task<WeatherModel> getWeatherByCity(string cityZip, string countryCode);
    }
}
