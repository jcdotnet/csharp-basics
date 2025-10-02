using Microsoft.AspNetCore.Mvc;
using MvcDemo.Models;

namespace MvcDemo.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            List<Person> people = [
                new Person() { Name = "John Doe", Gender = Gender.Male, BirthDate = Convert.ToDateTime("1980/06/03") },
                new Person() { Name = "Mary Doe", Gender = Gender.Male, BirthDate = Convert.ToDateTime("1980/06/03") },
                new Person() { Name = "Jane Doe", Gender = Gender.Male},
            ];
            ViewData["pageTitle"] = "ASP.NET core MVC Demo";
            ViewData["pageMessage"] = "Hello, World!";
            ViewData["people"] = people;
            // return new ViewResult() { ViewName="Index"}; // implements IActionResult
            return View();                                  // simplified, real world projects version  
            // return View("Home");                         // looks for Views/Home/Home.cshtml
        }
        [Route("details/{id}")]
        public IActionResult Details(int? id)
        {
            
            if (id is null) return BadRequest("Id cannot be empty");
            if (id > 3 ) return NotFound("Person Not Found");

            List<Person> people = [
                new Person() { Id = 1, Name = "John Doe", Gender = Gender.Male, BirthDate = Convert.ToDateTime("1980/06/03") },
                new Person() { Id = 2, Name = "Mary Doe", Gender = Gender.Male, BirthDate = Convert.ToDateTime("1980/06/03") },
                new Person() { Id = 3, Name = "Jane Doe", Gender = Gender.Male},
            ];
            Person? person = people.Where(p => p.Id == id).FirstOrDefault();
            return View(person);                            // using a strongly typed view instead of ViewData
        }
    }
}
