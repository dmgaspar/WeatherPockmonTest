using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherPockmonTest.Models
{
    public class PokemonInfo
    {
        public string LookUpCity { get; set; }

        public string CityStatus { get; set; }

        public string CityTemperature { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public string Type { get; set; }
    }
}