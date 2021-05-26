using Newtonsoft.Json;
using SunnyXamarin.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SunnyXamarin.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private string _baseURL = "http://api.openweathermap.org/data/2.5/weather";
        private string _apiKey = "appid=f491b607f1c1e1bb4af977bb5581ba86";

        public async Task<WeatherModel> GetWeatherByLocation(string longitude, string latitude)
        {
            try
            {
                var request = new HttpRequestMessage();
                string url = _baseURL + "?lat=" + latitude + "&lon=" + longitude + "&" + _apiKey;
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");
                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    var json = await content.ReadAsStringAsync();
                    WeatherModel weather = JsonConvert.DeserializeObject<WeatherModel>(json);

                    return weather;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;

        }

        public async Task<WeatherModel> GetWeatherByCity(string cityZip, string countryCode)
        {
            try
            {
                var request = new HttpRequestMessage();
                string url = _baseURL + "?zip=" + cityZip + "," + countryCode + "&" + _apiKey;
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");
                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    var json = await content.ReadAsStringAsync();
                    WeatherModel weather = JsonConvert.DeserializeObject<WeatherModel>(json);

                    return weather;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;

        }

        public double Conversion_CelvinToCelzius(double calvin)
        {
            return (calvin - 273.15);
        }

    }
}
