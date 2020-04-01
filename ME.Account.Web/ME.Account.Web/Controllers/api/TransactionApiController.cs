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
        public IHttpActionResult GetCustomerInfo(HttpRequestMessage request, string customerId)
        {
            return GetHttpResponse(request, () =>
            {
                var transResponse = _transactionInfoService.GetCustomerTransInfo(customerId, DateTime.Now, DateTime.Now);

                if (transResponse == null)
                {
                    return Content(HttpStatusCode.InternalServerError, "Error getting customer information!");
                }

                if (transResponse.Customer==null)
                {
                    return Content(HttpStatusCode.NotFound, String.Format("Customer {0} not found!", customerId));
                }

                return Ok(transResponse);
            });
        }
    }
}