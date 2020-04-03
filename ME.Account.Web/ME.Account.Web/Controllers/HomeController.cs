using ME.Account.Web.Core.Business;
using ME.Account.Web.Core.Contracts;
using ME.Account.Web.Models.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ME.Account.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICustomerInfoService _customerInfoService = new CustomerInfoService();

        public ActionResult Index()
        {
            var customerRequest = new CustomerRequest()
            {
                customerId = string.Empty,
                initialCredit = 0
            };
 
            return View(customerRequest);
        }

        [HttpPost]
        public ActionResult GetAccount(CustomerRequest customerRequest)
        {
           var customerAccount = _customerInfoService.RegisterAmount(customerRequest.customerId, customerRequest.initialCredit);

            return RedirectToAction("Index", "Transactions", customerAccount);
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is just a demo.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "You can contact if you want to...";

            return View();
        }
    }
}