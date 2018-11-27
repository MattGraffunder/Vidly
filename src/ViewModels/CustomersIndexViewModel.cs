using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomersIndexViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
    }
}