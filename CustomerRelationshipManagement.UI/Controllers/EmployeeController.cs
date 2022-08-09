using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerRelationshipManagement.UI.Models;
using CustomerRelationshipManagement.UI.Models.DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerRelationshipManagement.UI.Controllers
{
    public class EmployeeController:Controller
    {
        //private readonly RoleManager<Role> roleManager;
        //private readonly UserManager<Registration> userManager;
        //public EmployeeController(RoleManager<Role> roleManager, UserManager<Registration> userManager)
        //{
        //    this.roleManager = roleManager;
        //    this.userManager = userManager;
        //}
        private readonly ILogger<EmployeeController> _logger;
        private readonly CustomerRelationshipManagementDBContext _context;
        public EmployeeController(ILogger<EmployeeController> logger,CustomerRelationshipManagementDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index(int pg=1)
        {
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            var call = CustomerCallView();
            if (call == null)
            {
                List<CallByName> customer = new List<CallByName>();
                return View(customer);
            }
            int recsCount = call.Count();
            
            var pager = new Pager(recsCount, pg, pageSize);

            int resSkip = (pg - 1) * pageSize;

            //List<Customer> customers = _context.Customers.Skip(resSkip).Take(x).ToList();
            var views = CustomerCallView().Skip(resSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(views);
        }
        public IActionResult ManageCustomerCallDetails(string id)
        {
            return View(CustomerCallDetails(id));
        }
        [HttpGet]
        public IActionResult ManageCustomerCallEdit(string id)
        {
            return View(CustomerCallDetails(id));
        }
        [HttpPost]
        public IActionResult ManageCustomerCallEdit(CallByName customer)
        {
            CustomerCallEdit(customer);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ManageCustomerCallDelete(string id)
        {
            return View(CustomerCallDetails(id));
        }
        public IActionResult ManageCustomerCallDelete(CallByName customer)
        {
            //_context.Attach(customer);
            //_context.Entry(customer).State = EntityState.Deleted;
            //_context.SaveChanges();
            CustomerCallDelete(Convert.ToString(customer.CustomerCall_id));
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ManageCustomerCallCreate()
        {
            CallByName customer = new CallByName();
            return View(customer);
        }
        [HttpPost]
        public IActionResult ManageCustomerCallCreate(CallByName customer)
        {
            CustomerCallCreate(customer);
            return RedirectToAction("Index");
        }

        //calling store procedures
        public List<CallByName> CustomerCallView()
        {
            try
            {
                var x = _context.CustomerCallView();
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
        public CallByName CustomerCallDetails(string id)
        {
            return _context.CustomerCallDetails(id); ;
        }
        public CallByName CustomerCallEdit(CallByName customer)
        {
            return _context.CustomerCallEdit(customer); ;
        }
        public CallByName CustomerCallDelete(string id)
        {
            return _context.CustomerCallDelete(id);
        }
        public CallByName CustomerCallCreate(CallByName customer)
        {
            return _context.CustomerCallCreate(customer);
        }
    }
}
