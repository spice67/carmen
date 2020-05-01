using ME.Account.Web.Core;
using ME.Account.Web.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ME.Account.Web.Controllers.api
{
    public class TransactionApiController : ApiControllerBase
    {
        private readonly ITransactionInfoService _transactionInfoService;
        public TransactionApiController(ITransactionInfoService transactionInfoService)
        {
            _transactionInfoService = transactionInfoService;
        }

        [HttpGet]
        [Route("api/transactions/{customerId}")]
        public IHttpActionResult GetCustomerInfo(string customerId)
        {
            return GetHttpResponse(null,() =>
            {
                var customerInfo = _transactionInfoService.GetCustomerTransInfo(customerId, DateTime.Now, DateTime.Now);

                if (customerInfo == null)
                {
                    return Content(HttpStatusCode.InternalServerError, "Error getting customer information!");
                }

                if (customerInfo.Customer==null)
                {
                    return Content(HttpStatusCode.NotFound, String.Format("Customer {0} not found!", customerId));
                }

                double balance = 0.0;
                foreach (var item in customerInfo.Transactions)
                {
                    balance = balance + item.Amount;
                }

                dynamic result = new
                {
                    customerInfo,
                    balance
                };

                return Ok(result);
            });
        }
    }
}