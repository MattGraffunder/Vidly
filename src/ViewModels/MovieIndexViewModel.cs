using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieIndexViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
    }
}