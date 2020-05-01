using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ME.Account.Web.Core;
using ME.Account.Web.Core.Contracts;
using ME.Account.Web.Models.api;

namespace ME.Account.Web.Controllers.api
{
    public class CustomerApiController : ApiControllerBase
    {
        private readonly ICustomerInfoService _customerInfoService;
        public CustomerApiController(ICustomerInfoService customerInfoService)
        {
            _customerInfoService = customerInfoService;
        }

        [HttpPost]
        [Route("api/customers")]
        public IHttpActionResult CreateCustomerAccount([FromBody] CustomerRequest custReq)
        {
            return GetHttpResponse(null, () =>
            {
                var customerAccount = _customerInfoService.RegisterAmount(custReq.customerId, custReq.initialCredit);

                if (customerAccount == null)
                {
                    return Content(HttpStatusCode.NotFound, String.Format("Customer {0} not found!", custReq.customerId));
                }

                return Ok(customerAccount);
            });
        }

    }
}
