using Microsoft.AspNetCore.Mvc;
using MvcDemo.Models;

namespace MvcDemo.Controllers
{
    public class CustomerController : Controller
    {
        [Route("/customers")]
        public IActionResult Index()
        {
            List<Customer> customers = [
                new Customer() { Name = "John Doe", Gender = Gender.Male, BirthDate = Convert.ToDateTime("1980/06/03") },
                new Customer() { Name = "Mary Doe", Gender = Gender.Male, BirthDate = Convert.ToDateTime("1980/06/03") },
                new Customer() { Name = "Jane Doe", Gender = Gender.Male},
            ];
            ViewData["customers"] = customers;
            // return new ViewResult() { ViewName="Index"}; // implements IActionResult
            return View();                                  // simplified, real world projects version  
            // return View("Home");                         // looks for Views/Customer/Home.cshtml
        }

        [Route("customers/{id}")]
        public IActionResult Details(int? id)
        {       
            if (id is null) return BadRequest("Id cannot be empty");
            if (id > 3 ) return NotFound("Person Not Found");

            List<Customer> customers = [
                new Customer() { Id = 1, Name = "John Doe", Gender = Gender.Male, BirthDate = Convert.ToDateTime("1980/06/03") },
                new Customer() { Id = 2, Name = "Mary Doe", Gender = Gender.Male, BirthDate = Convert.ToDateTime("1980/06/03") },
                new Customer() { Id = 3, Name = "Jane Doe", Gender = Gender.Male},
            ];
            Customer? customer = customers.Where(p => p.Id == id).FirstOrDefault();
            return View(customer);                          // using a strongly typed view instead of ViewData
        }
    }
}
