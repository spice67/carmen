using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using ME.AccntRedis.Models;
using Core.Common.Contracts;
using ME.AccntRedis.Data.Hash;

namespace ME.AccntRedis.Data
{
    public interface ICustomerAccountRepository : IDataRepository<CustomerAccount>
    {
    }

    public class CustomerAccountRepository : DataRepositoryBase<CustomerAccount>, ICustomerAccountRepository
    {
        protected override CustomerAccount AddEntity(Hashtable entityContext, CustomerAccount entity)
        {
            string v = entity.CustomerId;
            var b = entityContext.ContainsValue(v);

            var ret = entity;

            if (!b)
            {
                // Possibility to create a customer with an account 
                entity.AccountNo = String.Format("ACNT{0}", new Random().Next(10).ToString());
                entityContext.Add(entity.AccountNo, entity.CustomerId);
                return entity;
            }
            else
            {
                return GetEntity(entityContext,v);
            }
        }

        protected override CustomerAccount UpdateEntity(Hashtable entityContext, CustomerAccount entity)
        {
            var b = entityContext.ContainsValue(entity.AccountNo);

            if (b)
            {
                //TODO
            }

            return entity;
        }

        protected override IEnumerable<CustomerAccount> GetEntities(Hashtable entityContext)
        {
            var l = new List<CustomerAccount>();

            foreach (DictionaryEntry item in entityContext.Keys)
            {
                l.Add(new CustomerAccount()
                {
                    CustomerId =  item.Value.ToString(),
                    AccountNo =  item.Key.ToString()
                });
            }

            return l;
        }

        protected override CustomerAccount GetEntity(Hashtable entityContext, string id)
        {
            var b = entityContext.ContainsValue(id);

            if (b)
            {
                foreach (var item in entityContext.Keys)
                {
                    if (id.Equals((string)entityContext[item]))
                    {
                        return new CustomerAccount()
                        {
                            AccountNo = (string)item,
                            CustomerId = id
                        };
                    }
                }
            }

            return null;
        }
    }
}