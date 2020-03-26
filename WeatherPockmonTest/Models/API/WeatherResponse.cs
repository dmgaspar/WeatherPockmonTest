using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherPockmonTest.Models.API
{
    public class WeatherResponse
    {
        public string Name { get; set; }

        public IEnumerable<WeatherDescription> Weather { get; set; }

        public Main Main { get; set; }
    }
}