using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Demo.Controllers
{
    public class BookController : Controller
    {
        // url example: http://localhost:5035/books/1
        [Route("/books/{id}")]
        public IActionResult Index()
        {
            int bookId = Convert.ToInt32(Request.RouteValues["id"]);
            Console.WriteLine(bookId);
            // id logic goes here...
            return File("/pg1513.txt", "text/plain"); // project gutenberg free book
        }

        // url example: http://localhost:5035/bookstore/?isloggedin=true&bookid=1
        // moved to http://localhost:5035/books/1
        [Route("/bookstore")]
        [Route("/store/books")]
        public IActionResult FindBook()
        {
            if (!Request.Query.ContainsKey("bookid")) 
            {
                // Response.StatusCode = 400;
                // return new BadRequestResult();
                return BadRequest("Book id not supplied"); ;
            }

            var bookId = Request.Query["bookid"];

            if (string.IsNullOrEmpty(Convert.ToString(bookId)))
            {
                // Response.StatusCode = 400;
                return BadRequest("Book id is not valid");
            }
            // checking id here rather than in router validation
            // to avoid non-reachable code (= see FindEmployee())
            if (Convert.ToInt32(bookId) < 0 || Convert.ToInt32(bookId) > 1000)
            {
                // Response.StatusCode = 400;
                return BadRequest("Book id is not valid");
            }
            if (Convert.ToBoolean(Request.Query["isloggedin"]) == false)
            {
                // Response.StatusCode = 401;
                // return Content("You must be authenticated to get this book");
                return Unauthorized("You must be authenticated to get this book");
            }

            // redirection (several ways)
            // 302
            // return new RedirectToActionResult("Index", "Book", new { id = bookId });
            // return RedirectToAction("Index", "Book", new { id = bookId });
            // return LocalRedirectResult($"books/{bookId}"); // also new LocalRedirectResult
            return Redirect($"books/{bookId}");

            //301
            // return new RedirectToActionResult("Index", "Book", new { id = bookId }, permanent: true); 
            // return RedirectToActionPermanent("Index", "Book", new { id = bookId });
            // return LocalRedirectPermanent($"books/{bookId}");
            // return RedirectPermanent($"books/{bookId}"); 
        }
    }
}
