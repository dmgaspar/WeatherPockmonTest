using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using WeatherPockmonTest.Models;

namespace WeatherPockmonTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LookUpPokemon(PokemonInfo pokemonInfo)
        {
            var pokemonService = new PokemonService();
            var pokemonResult =  await pokemonService.LookUpPokemon(pokemonInfo.LookUpCity);
            return View(pokemonResult);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Pokémon Challenge for Developers";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Dennis Martins Gaspar";

            return View();
        }
    }
}