using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Range(1, 20)]
        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }

        [Range(0, 20)]
        [Display(Name = "Number Available")]
        public int NumberAvailable { get; set; }

        public Genre Genre { get; set; }

        [Display(Name="Genre")]
        public int GenreId { get; set; }
    }
}