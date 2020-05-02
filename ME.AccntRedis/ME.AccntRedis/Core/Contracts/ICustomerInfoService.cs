using ME.AccntRedis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME.AccntRedis.Contracts
{
    public interface ICustomerInfoService
    {
        CustomerAccount RegisterAmount(string customerId, double initialAmount = 0);
    }
}
