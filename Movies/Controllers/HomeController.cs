using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName).ToList();
            return View("Movie", new MovieTemplate());
        }

        [HttpPost]
        public IActionResult Movie(MovieTemplate response)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(response); // adding a record to the database
                _context.SaveChanges();
                return View("Confirmation", response);
            }
            else
            {
                ViewBag.Categories = _context.Categories
                    .OrderBy(x => x.CategoryName).ToList();
                return View(response);
            }
        }

        public IActionResult ViewMovies()
        {
            var movies = _context.Movies
                .Include(x => x.Category)
                .OrderBy(x => x.Title).ToList();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordEdit = _context.Movies
                .Single(x => x.MovieId == id);
            ViewBag.Categories = _context.Categories
              .OrderBy(x => x.CategoryName).ToList();
            return View("Movie", recordEdit);
        }

        [HttpPost]
        public IActionResult Edit(MovieTemplate updatedInfo)
        {
            _context.Update(updatedInfo);
            _context.SaveChanges();
            return RedirectToAction("ViewMovies");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordDelete = _context.Movies
                .Single(x => x.MovieId == id);
            return View(recordDelete);
        }

        [HttpPost]
        public IActionResult Delete(MovieTemplate deletedInfo)
        {
            _context.Movies.Remove(deletedInfo);
            _context.SaveChanges();

            return RedirectToAction("ViewMovies");
        }
    }
}
