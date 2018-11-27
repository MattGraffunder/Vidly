using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET api/Rentals
        public IHttpActionResult GetRentals()
        {
            return null;
        }

        // GET api/Rentals/5
        public IHttpActionResult GetRental(int rentalId)
        {
            return null;
        }

        // POST api/Rentals
        [HttpPost]
        public IHttpActionResult New(RentalDto rentalDto)
        {
            if (rentalDto.MovieIds == null || rentalDto.MovieIds.Count ==0)
            {
                return BadRequest("No Movies Provided.");
            }

            //Get Customer
            var customer = _context.Customers.FirstOrDefault(c => c.Id == rentalDto.CustomerId);

            if (customer == null)
            {
                return BadRequest("Customer Id is not valid");
            }
            
            //Create Rentals for movies
            foreach (var movieId in rentalDto.MovieIds)
            {
                var movie = _context.Movies.FirstOrDefault(m => m.Id == movieId);

                if (movie == null)
                {
                    return BadRequest("Movie Id is not valid");
                }

                if (movie.NumberAvailable == 0)
                {
                    return BadRequest("Movie is not available");
                }

                //Add Rental
                _context.Rentals.Add(new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    RentalDate = DateTime.Now
                });

                //Decrement Number Avaialable for Movie
                movie.NumberAvailable--;                
            }

            //Save Rentals
            _context.SaveChanges();

            return Ok();
        }

        // PUT api/Rentals/5
        [HttpPut]
        public IHttpActionResult Put(int id, RentalDto rentalDto)
        {
            return null;
        }

        // DELETE api/Rentals/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            return null;
        }
    }
}