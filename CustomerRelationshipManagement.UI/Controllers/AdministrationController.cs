using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CustomerRelationshipManagement.UI.Models.DBContext;
using CustomerRelationshipManagement.UI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace CustomerRelationshipManagement.UI.Controllers
{
    //[Authorize(Roles = "Manager")]
    public class AdministrationController:Controller
    {
        
        private readonly ILogger<AdministrationController> _logger;
        private readonly CustomerRelationshipManagementDBContext _context;

        public AdministrationController(ILogger<AdministrationController> logger, CustomerRelationshipManagementDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index(int pg=1)
        {
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            var call = CustomerView();
            if (call == null)
            {
                List<Customer> customer = new List<Customer>();
                return View(customer);
            }

            int recsCount= call.Count();
            var pager = new Pager(recsCount, pg, pageSize);

            int resSkip = (pg - 1) * pageSize;
            
            //List<Customer> customers = _context.Customers.Skip(resSkip).Take(x).ToList();
            var views = CustomerView().Skip(resSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(views);
        }
        public IActionResult ManageCustomerDetails(string id)
        {
            //Customer customer = _context.Customers.Where(p => p.CustomerNo.ToString() == id).FirstOrDefault();
            var x = CustomerDetails(id);
            return View(x);
        }
        [HttpGet]
        public IActionResult ManageCustomerEdit(string id)
        {
            //Customer customer = _context.Customers.Where(p => p.CustomerNo.ToString() == id).FirstOrDefault();
            var x = CustomerDetails(id);
            return View(x);
        }
        [HttpPost]
        public IActionResult ManageCustomerEdit(Customer customer)
        {
            //_context.Attach(customer);
            //_context.Entry(customer).State = EntityState.Modified;
            //_context.SaveChanges();
            CustomerEdit(customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ManageCustomerDelete(string id)
        {
            //Customer customer = _context.Customers.Where(p => p.CustomerNo.ToString() == id).FirstOrDefault();
            var x = CustomerDetails(id);
            return View(x);
        }
        [HttpPost]
        public IActionResult ManageCustomerDelete(Customer customer)
        {
            //_context.Attach(customer);
            //_context.Entry(customer).State = EntityState.Deleted;
            //_context.SaveChanges();
            CustomerDelete(Convert.ToString(customer.CustomerNo));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ManageCustomerCreate()
        {
            Customer customer = new Customer();
            return View(customer);
        }
        [HttpPost]
        public IActionResult ManageCustomerCreate(Customer customer)
        {
            //_context.Attach(customer);
            //_context.Entry(customer).State = EntityState.Added;
            //_context.SaveChanges();
            CustomerCreate(customer);
            return RedirectToAction("Index");
        }

        //calling store procedures
        public List<Customer> CustomerView()
        {
            try
            {
                var x = _context.CustomerView();
                if (x.Count < 1)
                {
                    x = null;
                }
                return x;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Customer CustomerDetails(string id)
        {
            return _context.CustomerDetails(id); ;
        }
        public Customer CustomerEdit(Customer customer)
        {
            return _context.CustomerEdit(customer); ;
        }
        public Customer CustomerDelete(string id)
        {
            return _context.CustomerDelete(id);
        }
        public Customer CustomerCreate(Customer customer)
        {
            return _context.CustomerCreate(customer); ;
        }
        //Start manage report
        [Authorize(Roles = "Manager")]
        public IActionResult ManageReport()
        {
            return View();
        }

    }
}
