using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.DTOs
{
    public class RentalDto
    {
        public int CustomerId { get; set; }
        public IList<int> MovieIds { get; set; }
    }
}