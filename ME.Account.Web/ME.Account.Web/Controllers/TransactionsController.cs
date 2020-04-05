using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ME.Account.Web.Core.Contracts;
using ME.Account.Web.Core.Business;
using ME.Account.Web.Models.api;

namespace ME.Account.Web.Controllers
{
    public class TransactionsController : Controller
    {
        private ITransactionInfoService transactionInfoService = new TransactionInfoService();

        public ActionResult Index(CustomerAccount customerAccount)
        {
            var transResponse = transactionInfoService.GetCustomerTransInfo(customerAccount.CustomerId, DateTime.Now, DateTime.Now);

            double balance = 0;

            foreach (var item in transResponse.Transactions)
            {
                balance = balance + item.Amount;
            }

            ViewBag.Balance = balance;

            return View(transResponse);
        }
    }
}