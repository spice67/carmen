using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using ME.AccntRedis.Models;
using Core.Common.Contracts;
using ME.AccntRedis.Data.Hash;
using Common.Core.Contracts;
using StackExchange.Redis;

namespace ME.AccntRedis.Data
{
    public interface ICustomerAccountRepository : IDataRepository<CustomerAccount>
    {
    }

    public class CustomerAccountRepository : DataRepositoryBase<CustomerAccount>, ICustomerAccountRepository
    {
        public CustomerAccountRepository(IRedisContext ctx): base(ctx)
        {

        }
        protected override CustomerAccount AddEntity(RedisContext entityContext, CustomerAccount entity)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<CustomerAccount> GetEntities(RedisContext entityContext, string k)
        {
            throw new NotImplementedException();
        }

        protected override CustomerAccount GetEntity(RedisContext entityContext, string id)
        {
            var d = entityContext.GetDb();

            var account = d.StringGet("ACCNT:" + id);

            if (account.HasValue)
            {
                return new CustomerAccount()
                {
                    CustomerId = id,
                    AccountNo = account
                };
            }

            return null;
        }

        protected override CustomerAccount Remove(RedisContext entityContext, CustomerAccount entity)
        {
            throw new NotImplementedException();
        }

        protected override CustomerAccount UpdateEntity(RedisContext entityContext, CustomerAccount entity)
        {
            throw new NotImplementedException();
        }

    }
}