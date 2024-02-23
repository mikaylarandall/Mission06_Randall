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
        // putting the other table categories into a viewbag
        [HttpGet]
        public IActionResult Movie()
        {
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName).ToList();
            return View("Movie", new MovieTemplate());
        }
        
        // making sure the model is validated in the movie application, and adding the data to the database
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
        // showing the table with all of the movies in the database
        public IActionResult ViewMovies()
        {
            var movies = _context.Movies
                .Include(x => x.Category)
                .OrderBy(x => x.Title).ToList();
            return View(movies);
        }
        // receiving the movie that the individual would like to edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordEdit = _context.Movies
                .Single(x => x.MovieId == id);
            ViewBag.Categories = _context.Categories
              .OrderBy(x => x.CategoryName).ToList();
            return View("Movie", recordEdit);
        }
        // updating and saving the data with the desired change
        [HttpPost]
        public IActionResult Edit(MovieTemplate updatedInfo)
        {
            _context.Update(updatedInfo);
            _context.SaveChanges();
            return RedirectToAction("ViewMovies");
        }
        // receiving the movie that the individual would like to delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordDelete = _context.Movies
                .Single(x => x.MovieId == id);
            return View(recordDelete);
        }
        // removing the movie from the database
        [HttpPost]
        public IActionResult Delete(MovieTemplate deletedInfo)
        {
            _context.Movies.Remove(deletedInfo);
            _context.SaveChanges();

            return RedirectToAction("ViewMovies");
        }
    }
}
