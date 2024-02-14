using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using System.Diagnostics;

namespace Movies.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext _context;
        public HomeController(MovieContext temp) // Constructor
        {
            _context = temp;
            // builds an instance of the database
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetToKnowHim()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Movie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Movie(MovieTemplate response)
        {
            _context.Movies.Add(response); // adding a record to the database
            _context.SaveChanges();
            return View("Confirmation", response);
        }

    }
}
