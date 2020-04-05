using ME.Account.Web.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ME.Account.Web.Models.api;
using Core.Common.Contracts;
using ME.Account.Web.Core.Data;

namespace ME.Account.Web.Core.Business
{
    public class CustomerInfoService : ICustomerInfoService
    {
        private IDataRepository<CustomerAccount> _customerAccountRepo = new CustomerAccountRepository();
        private IDataRepository<Transaction> _transactionRepo = new TransactionRepository();

        public CustomerAccount RegisterAmount(string customerId, double initialAmount = 0)
        {
            var customerAccount = _customerAccountRepo.Get(customerId);

            if (customerAccount != null) 
            {
                string transId = String.Format("TRAN_{0}", Guid.NewGuid().ToString());
                _transactionRepo.Add(new Transaction()
                {
                    AccountNo = customerAccount.AccountNo,
                    Amount = initialAmount,
                    Id = transId,
                    transDate = DateTime.Now.Date
                });
            }
            return customerAccount;
        }
    }
}