using ME.AccntRedis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Newtonsoft.Json;
using Core.Common.Contracts;
using ME.AccntRedis.Data.Hash;

namespace ME.AccntRedis.Data
{
    public interface ICustomerRepository : IDataRepository<Customer>
    {
    }

    public class CustomerRepository : DataRepositoryBase<Customer>, ICustomerRepository
    {
        protected override Customer AddEntity(Hashtable entityContext, Customer entity)
        {
            var b = entityContext.ContainsKey(entity.Id);

            if (!b)
            {
                entityContext.Add(entity.Id, entity.ToJson());
            }

            return entity;
        }

        protected override IEnumerable<Customer> GetEntities(Hashtable entityContext)
        {
            var customers = new List<Customer>();

            foreach (DictionaryEntry item in entityContext)
            {
                var customer = JsonConvert.DeserializeObject<Customer>((string)item.Value);
                customers.Add(customer);
            }

            return customers;
        }

        protected override Customer GetEntity(Hashtable entityContext, string id)
        {
            var b = entityContext.ContainsKey(id);

            if (b)
            {
                var customer = JsonConvert.DeserializeObject<Customer>( (string)entityContext[id]);

                return customer;
            }

            return new Customer()
            {
                Id = id,
                Name = string.Empty,
                Surname = string.Empty
            };
        }

        protected override Customer UpdateEntity(Hashtable entityContext, Customer entity)
        {
            //TODO
            return null;
        }
    }
}