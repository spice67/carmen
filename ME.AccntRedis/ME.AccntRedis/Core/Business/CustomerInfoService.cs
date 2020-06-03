using ME.Account.Web.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ME.AccntRedis.Models;
using Core.Common.Contracts;
using ME.AccntRedis.Data;
using ME.AccntRedis.Contracts;

namespace ME.Account.Web.Core.Business
{
    public class CustomerInfoService : ICustomerInfoService
    {
        private IDataRepository<CustomerAccount> _customerAccountRepo;
        private IDataRepository<Transaction> _transactionRepo;

        public CustomerInfoService(ICustomerAccountRepository customerAccountRepository, ITransactionRepository transactionRepository)
        {
            _customerAccountRepo = customerAccountRepository;
            _transactionRepo = transactionRepository;
        }

        public CustomerAccount RegisterAmount(string customerId, double initialAmount = 0)
        {
            var customerAccount = _customerAccountRepo.GetSingle(customerId);

            if ((customerAccount != null) && (initialAmount != 0))
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