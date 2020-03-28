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
  
        public async Task<PokemonInfo> LookUpPokemon(string city)
        {
            try
            {
                PokemonInfo pokemonInfo = new PokemonInfo();

                pokemonInfo.LookUpCity = city;

                await SetTemperatureAndStatusAsync(pokemonInfo);

                SetPokemonType(pokemonInfo);

                await SetPokemonAsync(pokemonInfo);

                return pokemonInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetPokemonType(PokemonInfo pokemonInfo)
        {
            float temperature = float.Parse(pokemonInfo.CityTemperature);

            pokemonInfo.CityStatus = pokemonInfo.CityStatus == "Rain" ? "rainning" : "not rainning";

            if (pokemonInfo.CityStatus == PokemonType.Electric)
            {
                pokemonInfo.Type = PokemonType.Electric;
            }
            else if (temperature < 5)
            {
                pokemonInfo.Type = PokemonType.Ice;
            }
            else if (temperature >= 5 && temperature < 10)
            {
                pokemonInfo.Type = PokemonType.Water;
            }
            else if (temperature >= 12 && temperature < 15)
            {
                pokemonInfo.Type = PokemonType.Grass;
            }
            else if (temperature >= 15 && temperature < 21)
            {
                pokemonInfo.Type = PokemonType.Ground;
            }
            else if (temperature >= 23 && temperature < 27)
            {
                pokemonInfo.Type = PokemonType.Bug;
            }
            else if (temperature >= 27 && temperature <= 33)
            {
                pokemonInfo.Type = PokemonType.Rock;
            }
            else if (temperature > 33)
            {
                pokemonInfo.Type = PokemonType.Fire;
            }
            else
            {
                pokemonInfo.Type = PokemonType.Normal;
            }
        }

        private async Task SetTemperatureAndStatusAsync(PokemonInfo pokmonInfo)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={pokmonInfo.LookUpCity}&appid={appIdNumber}&units=metric");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<WeatherResponse>(stringResult);

                    pokmonInfo.CityTemperature = rawWeather.Main.Temp;
                    pokmonInfo.CityStatus = string.Join(",", rawWeather.Weather.Select(x => x.Main));

                }
                catch (HttpRequestException httpRequestException)
                {
                    throw new Exception(httpRequestException.Message);
                }
            }
        }

        private async Task SetPokemonAsync(PokemonInfo pokemonInfo)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://pokeapi.co");
                    var response = await client.GetAsync($"/api/v2/type/{pokemonInfo.Type}");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawPokemon = JsonConvert.DeserializeObject<PokemonResponse>(stringResult);

                    SetPokemonName(rawPokemon, pokemonInfo);
                }
                catch (HttpRequestException httpRequestException)
                {
                    throw new Exception(httpRequestException.Message);
                }
            }
        }

        private void SetPokemonName(PokemonResponse rawPokemon, PokemonInfo pokemonInfo)
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
                    pokemonInfo.Name = pokemon.Pokemon.Name;
                    pokemonInfo.URL = pokemon.Pokemon.URL;
                    lastPockemonUsed = pokemon.Pokemon.Name;
                    next = false;
                }
            } while (next);
        }
    }
}