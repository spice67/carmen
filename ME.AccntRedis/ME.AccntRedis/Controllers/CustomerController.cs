using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ME.AccntRedis.Contracts;
using ME.AccntRedis.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ME.AccntRedis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ApiControllerBase
    {

        private readonly ICustomerInfoService _customerInfoService;

        public CustomerController(ICustomerInfoService customerInfoService)
        {
            _customerInfoService = customerInfoService;
        }

        [HttpPost]
        public IActionResult CreateCustomerAccount(CustomerRequest customerRequest)
        {
            return GetHttpResponse(null, () =>
            {
                var customerAccount = _customerInfoService.RegisterAmount(customerRequest.customerId, customerRequest.initialCredit);

                if (customerAccount == null)
                {
                    return NotFound(String.Format("Customer {0} not found!", customerRequest.customerId));
                }

                return Ok (customerAccount);
            });
        }
    }
}