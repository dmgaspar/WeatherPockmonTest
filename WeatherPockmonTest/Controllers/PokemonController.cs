using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeatherPockmonTest.Models;


namespace WeatherPockmonTest.Controllers
{
    public class PokemonController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> City(string name)
        //{
        //    name = "Campinas";
        //    Pokemon pokemon = new Pokemon();
        //    using (var client = new HttpClient())
        //    {
        //        try
        //        {
        //            client.BaseAddress = new Uri("http://api.openweathermap.org");
        //            var test = $"/data/2.5/weather?q={name}&appid={appIdNumber}&units=metric";
        //            var response = await client.GetAsync($"/data/2.5/weather?q={name}&appid={appIdNumber}&units=metric");
        //            response.EnsureSuccessStatusCode();

        //            var stringResult = await response.Content.ReadAsStringAsync();
        //            var rawWeather = JsonConvert.DeserializeObject<WeatherResponse>(stringResult);

        //            pokemon.Name = name;
        //            pokemon.Temperature = rawWeather.Main.Temp;

        //        }
        //        catch (HttpRequestException httpRequestException)
        //        {
        //            ModelState.AddModelError("Error", $"Error getting weather from OpenWeather: {httpRequestException.Message}");
        //            return View();
        //        }

        //        return View(pokemon);

        //    }
        //}
    }
}