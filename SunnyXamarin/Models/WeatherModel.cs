﻿using Newtonsoft.Json;
using System;

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
        public string Cityname { get; set; }

        public class Weather
        {
            //[{"id":804,"main":"Clouds","description":"overcast clouds","icon":"04d"}]

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("main")]
            public string Main { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("icon")]
            public string Icon { get; set; }

        }

        public class MainW
        {
            //{"temp":286.32,"feels_like":285.42,"temp_min":283.61,"temp_max":286.64,"pressure":1011,"humidity":66}

            [JsonProperty("temp")]
            public double Temp { get; set; }

            [JsonProperty("feels_like")]
            public double Feels_like { get; set; }

            [JsonProperty("temp_min")]
            public double Temp_min { get; set; }

            [JsonProperty("temp_max")]
            public double Temp_max { get; set; }

            [JsonProperty("pressure")]
            public int Pressure { get; set; }

            [JsonProperty("humidity")]
            public int Humidity { get; set; }


    }

        public class Wind
        {
            //{"speed":2.83,"deg":210,"gust":6.31}
            [JsonProperty("speed")]
            public float Speed { get; set; }

            [JsonProperty("deg")]
            public int Deg { get; set; }

            [JsonProperty("gust")]
            public float Gust { get; set; }
        }

        public class Clouds
        {
            //{"all":100}
            [JsonProperty("all")]
            public int All { get; set; }
    }
    }
}
