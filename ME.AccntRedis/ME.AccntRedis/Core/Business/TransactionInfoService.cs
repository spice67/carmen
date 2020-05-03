using ME.Account.Web.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ME.AccntRedis.Models;
using ME.AccntRedis.Contracts;
using Core.Common.Contracts;
using ME.AccntRedis.Data;

namespace ME.Account.Web.Core.Business
{
    public class TransactionInfoService : ITransactionInfoService
    {
        private IDataRepository<Customer> _customerRepo;
        private IDataRepository<CustomerAccount> _customerAccountRepo;
        private IDataRepository<Transaction> _transactionRepo;

        public TransactionInfoService(ICustomerRepository customerRepository, ICustomerAccountRepository customerAccountRepository, 
            ITransactionRepository transactionRepository)
        {
            _customerRepo = customerRepository;
            _customerAccountRepo = customerAccountRepository;
            _transactionRepo = transactionRepository;
        }

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