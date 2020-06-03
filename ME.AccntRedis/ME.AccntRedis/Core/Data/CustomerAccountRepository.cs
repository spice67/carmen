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

        //protected override CustomerAccount AddEntity(Hashtable entityContext, CustomerAccount entity)
        //{
        //    string v = entity.CustomerId;
        //    var b = entityContext.ContainsValue(v);

        //    var ret = entity;

        //    if (!b)
        //    {
        //        // Possibility to create a customer with an account 
        //        entity.AccountNo = String.Format("ACNT{0}", new Random().Next(10).ToString());
        //        entityContext.Add(entity.AccountNo, entity.CustomerId);
        //        return entity;
        //    }
        //    else
        //    {
        //        return GetEntity(entityContext,v);
        //    }
        //}

        //protected override CustomerAccount UpdateEntity(Hashtable entityContext, CustomerAccount entity)
        //{
        //    var b = entityContext.ContainsValue(entity.AccountNo);

        //    if (b)
        //    {
        //        //TODO
        //    }

        //    return entity;
        //}

        //protected override IEnumerable<CustomerAccount> GetEntities(Hashtable entityContext)
        //{
        //    var l = new List<CustomerAccount>();

        //    foreach (DictionaryEntry item in entityContext.Keys)
        //    {
        //        l.Add(new CustomerAccount()
        //        {
        //            CustomerId =  item.Value.ToString(),
        //            AccountNo =  item.Key.ToString()
        //        });
        //    }

        //    return l;
        //}

        //protected override CustomerAccount GetEntity(Hashtable entityContext, string id)
        //{
        //    var b = entityContext.ContainsValue(id);

        //    if (b)
        //    {
        //        foreach (var item in entityContext.Keys)
        //        {
        //            if (id.Equals((string)entityContext[item]))
        //            {
        //                return new CustomerAccount()
        //                {
        //                    AccountNo = (string)item,
        //                    CustomerId = id
        //                };
        //            }
        //        }
        //    }

        //    return null;
        //}
    }
}