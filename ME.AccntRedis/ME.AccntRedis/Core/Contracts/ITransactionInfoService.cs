using ME.AccntRedis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME.Account.Web.Core.Contracts
{
    public interface ITransactionInfoService
    {
        TransactionResponse GetCustomerTransInfo(string customerId, DateTime transactionStart, DateTime transactionEnd);
    }
}
