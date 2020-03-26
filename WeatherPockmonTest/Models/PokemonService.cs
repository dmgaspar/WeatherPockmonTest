using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WeatherPockmonTest.Models.Data;
using WeatherPockmonTest.Models.Data.Json;

namespace WeatherPockmonTest.Models
{
    public class PokemonService
    {

        private string lastPockemonUsed = string.Empty;

        private readonly string appIdNumber = "a8bb6de7c0b7852044690af83e8537b0";
        public async Task<PokemonInfo> City(string city)
        {
            city = "Brasilia";

            PokemonInfo pokemonCityWeather = new PokemonInfo();

            await GetCityWeatherAsync(city, pokemonCityWeather);

            //string pockemonType = GetThePockemonType(cityWeather);

            await GetPokemonAsync("bug", pokemonCityWeather);

            return pokemonCityWeather;

        }



        private string GetThePockemonType(PokemonInfo cityWeather)
        {
            float temperature = float.Parse(cityWeather.CityTemperature);
            String pockemonType = string.Empty;

            if (cityWeather.CityStatus == "Rain")
            {
                pockemonType = "electric";
            }
            else if (temperature < 5)
            {
                pockemonType = "ice";
            }
            else if (temperature >= 5 && temperature < 10)
            {
                pockemonType = "water";
            }
            else if (temperature >= 12 && temperature < 15)
            {
                pockemonType = "grass";
            }
            else if (temperature >= 15 && temperature <= 21)
            {
                pockemonType = "ground";
            }
            else if (temperature >= 23 && temperature <= 27)
            {
                pockemonType = "bug";
            }
            else if (temperature >= 27 && temperature <= 33)
            {
                pockemonType = "rock";
            }
            else if (temperature > 33)
            {
                pockemonType = "fire";
            }
            else
            {
                pockemonType = "normal";
            }
            return pockemonType;
        }

        private async Task GetCityWeatherAsync(string city, PokemonInfo cityWeather)
        {


            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid={appIdNumber}&units=metric");
                    response.EnsureSuccessStatusCode();


                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<WeatherResponse>(stringResult);

                    cityWeather.CityTemperature = rawWeather.Main.Temp;
                    cityWeather.CityStatus = string.Join(",", rawWeather.Weather.Select(x => x.Main));


                }
                catch (HttpRequestException httpRequestException)
                {
                    var error = httpRequestException.Message;

                    // ModelState.AddModelError("Error", $"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }

        private async Task GetPokemonAsync(string pockemonType, PokemonInfo pokemonCityWeather)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://pokeapi.co");
                    var response = await client.GetAsync($"/api/v2/type/{pockemonType}");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawPokemon = JsonConvert.DeserializeObject<PokemonResponse>(stringResult);

                    GetPokemonName(rawPokemon, pokemonCityWeather);

                    //cityWeather.Status = string.Join(",", rawWeather.Weather.Select(x => x.Main));
                }
                catch (HttpRequestException httpRequestException)
                {
                    var error = httpRequestException.Message;

                    // ModelState.AddModelError("Error", $"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }

        private void GetPokemonName(PokemonResponse rawPokemon, PokemonInfo pokemonCityWeather)
        {
            int count = rawPokemon.pokemon.Count();
            Random rnd = new Random();
            int index = 0;
            bool next = true;
            do
            {
                index = rnd.Next(count);
                var pokemon = rawPokemon.pokemon.ElementAt(index);
                if (pokemon.Pokemon.Name != lastPockemonUsed)
                {
                    pokemonCityWeather.Name = pokemon.Pokemon.Name;
                    pokemonCityWeather.URL = pokemon.Pokemon.URL;
                    lastPockemonUsed = pokemon.Pokemon.Name;
                    next = false;
                }
            } while (next);
        }
    }
}