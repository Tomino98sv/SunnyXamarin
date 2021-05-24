using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunnyXamarin.Models
{
    [JsonObject]
    public class WeatherModel
    {
        [JsonProperty("weather")]
        public Weather[] WeatherSimple { get; set; }

        [JsonProperty("visibility")]
        public Int16 WeatherVisibility { get; set; }

        [JsonProperty("main")]
        public MainW WeatherMain { get; set; }

        [JsonProperty("wind")]
        public Wind WeatherWind { get; set;}

        [JsonProperty("clouds")]
        public Clouds WeatherClouds { get; set; }

        [JsonProperty("name")]
        public string cityname { get; set; }

        public class Weather
        {
            //[{"id":804,"main":"Clouds","description":"overcast clouds","icon":"04d"}]

            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("main")]
            public string main { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }

            [JsonProperty("icon")]
            public string icon { get; set; }

        }

        public class MainW
        {
            //{"temp":286.32,"feels_like":285.42,"temp_min":283.61,"temp_max":286.64,"pressure":1011,"humidity":66}

            [JsonProperty("temp")]
            public float temp { get; set; }

            [JsonProperty("feels_like")]
            public float feels_like { get; set; }

            [JsonProperty("temp_min")]
            public float temp_min { get; set; }

            [JsonProperty("temp_max")]
            public float temp_max { get; set; }

            [JsonProperty("pressure")]
            public int pressure { get; set; }

            [JsonProperty("humidity")]
            public int humidity { get; set; }


    }

        public class Wind
        {
            //{"speed":2.83,"deg":210,"gust":6.31}
            [JsonProperty("speed")]
            public float speed { get; set; }

            [JsonProperty("deg")]
            public int deg { get; set; }

            [JsonProperty("gust")]
            public float gust { get; set; }
        }

        public class Clouds
        {
            //{"all":100}
            [JsonProperty("all")]
            public int all { get; set; }
    }
    }
}
