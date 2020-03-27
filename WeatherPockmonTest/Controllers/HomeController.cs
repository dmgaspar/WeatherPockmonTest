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
            var pokemonInfo = new PokemonInfo();
            return View(pokemonInfo);
        }

        [HttpPost]
        public async Task<ActionResult> LookUpPokemon(PokemonInfo pokemonInfo)
        {
            var pokemonResult = new PokemonInfo();
            if (pokemonInfo != null && pokemonInfo.LookUpCity != null)
            {
                var pokemonService = new PokemonService();
                pokemonResult = await pokemonService.LookUpPokemon(pokemonInfo.LookUpCity);
                return View(pokemonResult);
            }
            else 
            {
                return View(pokemonResult);
            }
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