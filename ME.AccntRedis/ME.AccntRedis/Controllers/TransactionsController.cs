using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ME.Account.Web.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ME.AccntRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ApiControllerBase
    {
        private readonly ITransactionInfoService _transactionInfoService;

        public TransactionsController(ITransactionInfoService transactionInfoService)
        {
            _transactionInfoService = transactionInfoService;
        }

        [HttpGet("{customerId}")]
        public IActionResult GetCustomerInfo(string customerId)
        {
            return GetHttpResponse(null, () =>
            {
                var customerInfo = _transactionInfoService.GetCustomerTransInfo(customerId, DateTime.Now, DateTime.Now);

                if (customerInfo == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error getting customer information!");
                }

                if (customerInfo.Customer == null)
                {
                    return NotFound(String.Format("Customer {0} not found!", customerId));
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