using ME.Account.Web.Models.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME.Account.Web.Core.Contracts
{
    public interface ICustomerInfoService
    {
        CustomerAccount RegisterAmount(string customerId, double initialAmount = 0);
    }
}
