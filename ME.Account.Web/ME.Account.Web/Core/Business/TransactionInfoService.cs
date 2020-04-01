using ME.Account.Web.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ME.Account.Web.Models.api;
using ME.Account.Web.Core.Data;
using Core.Common.Contracts;

namespace ME.Account.Web.Core.Business
{
    public class TransactionInfoService : ITransactionInfoService
    {
        private IDataRepository<Customer> _customerRepo = new CustomerRepository();
        private IDataRepository<CustomerAccount> _customerAccountRepo = new CustomerAccountRepository();
        private IDataRepository<Transaction> _transactionRepo = new TransactionRepository();
        public TransactionResponse GetCustomerTransInfo(string customerId, DateTime transactionStart, DateTime transactionEnd)
        {
            var transactionResponse = new TransactionResponse();

            var customerAccount = _customerAccountRepo.Get(customerId);

            if (customerAccount == null)
            {
                // return immediately with a nulled customerResponse properties
                return transactionResponse;
            }

            transactionResponse.Customer = _customerRepo.Get(customerId);

            //var transactions = _transactionRepo.Get().AsQueryable().Select(x => x.AccountNo == customerAccount.AccountNo);

            transactionResponse.Transactions = _transactionRepo.Get().Where(x => x.AccountNo == customerAccount.AccountNo).ToList();

            return transactionResponse;
        }
    }
}