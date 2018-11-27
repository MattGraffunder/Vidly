using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _context.Dispose();
        }

        // GET: Customers
        public ViewResult Index()
        {
            //var vm = new CustomersIndexViewModel
            //{
            //    Customers = _context.Customers.Include(c => c.MembershipType)
            //};

            //return View(vm);
            return View();
        }


        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).FirstOrDefault(c => c.Id == id);

            if (customer is null) { return HttpNotFound(); }

            return View(customer);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var vm = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes
                };

                return View("CustomerForm", vm);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.First(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customer.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

            if(customer == null)
            {
                return HttpNotFound();
            }

            var vm = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", vm);
        }
    }
}