using Newtonsoft.Json;
using SunnyXamarin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SunnyXamarin.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        const string baseURL= "http://api.openweathermap.org/data/2.5/weather";
        const string apiKey = "appid=f491b607f1c1e1bb4af977bb5581ba86";

        public async Task<WeatherModel> getWeatherByLocation(string longitude, string latitude)
        {
            try
            {
                var request = new HttpRequestMessage();
                string url = baseURL + "?lat=" + latitude + "&lon=" + longitude + "&" + apiKey;
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

    }
}
