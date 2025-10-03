using Microsoft.AspNetCore.Mvc;
using WeatherMvcApp.Models;

namespace WeatherMvcApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly List<CityWeather> cities = [
            new() { Code = "LDN", Name = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"), Temperature = 33 },
            new() { Code = "NYC", Name = "New York", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"), Temperature = 60 },
            new() { Code = "PAR", Name = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"), Temperature = 82 }
        ];

        [Route("/")]
        public IActionResult Index()
        {
            return View(cities); 
        }

        [Route("weather/{cityCode?}")]
        public IActionResult City(string cityCode)
        {
            if (string.IsNullOrEmpty(cityCode))
            {
                return View();
            }

            // FirstOrDefault: city is null if we entered a city code that does not exist
            CityWeather? city = cities.Where(temp => temp.Code == cityCode).FirstOrDefault();
            return View(city);
        }
    }
}
