using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherPockmonTest.Models.Data.Json
{
    public class PokemonResponse
    {
        public string Name { get; set; }

        public IEnumerable<PokemonItem> pokemon { get; set; }

    }
}