using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.ViewModels;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _context.Dispose();
        }
        
        // GET: Movies
        public ActionResult Random()
        {
            Movie movie = new Movie { Name = "Shrek!" };

            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        public ActionResult Index()
        {
            //if (MemoryCache.Default["Genres"] == null)
            //{
            //    MemoryCache.Default["Genres"] = _context.Genres.ToList();
            //}

            //var genres = MemoryCache.Default["Genres"];

            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View("List");

            }
            else
            {
                return View("ReadOnlyList");
            }
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);

            if(movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        [Authorize(Roles =RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres;

            var vm = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = genres
            };

            return View("Edit", vm);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var genres = _context.Genres;

            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var vm = new MovieFormViewModel
            {
                Movie = movie,
                Genres = genres
            };

            return View("Edit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var vm = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres
                };

                return View("Edit", vm);
            }

            if (movie.Id == 0)
            {
                //new movie
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);

            }
            else
            {
                var movieInDb = _context.Movies.First(m => m.Id == movie.Id);

                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}