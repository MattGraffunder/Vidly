using AutoMapper;
using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    [Authorize]
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/Movies
        public IHttpActionResult GetMovies(string query = null)
        {
            var moviesQuery =_context.Movies
                .Include(m => m.Genre);

            if (!String.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query) && m.NumberAvailable > 0);
            }
            
            var movieDtos = moviesQuery.Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDtos);
        }

        // GET /api/Movies/{id}
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

            if(movie == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/Movies
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            movie.DateAdded = DateTime.Now;

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri($"{Request.RequestUri}/{movieDto.Id}"), movieDto);
        }

        // PUT /api/Movies/{id}
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            Mapper.Map(movieDto, movie);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/Movies/{id}
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok();
        }
    }
}