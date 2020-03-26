using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherPockmonTest.Models.Data.Json
{
    public class WeatherResponse
    {
        public IEnumerable<WeatherDescription> Weather { get; set; }

        public Main Main { get; set; }
    }
}