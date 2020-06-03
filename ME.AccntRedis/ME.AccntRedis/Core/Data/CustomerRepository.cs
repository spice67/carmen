using ME.AccntRedis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Newtonsoft.Json;
using Core.Common.Contracts;
using ME.AccntRedis.Data.Hash;
using Common.Core.Contracts;

namespace ME.AccntRedis.Data
{
    public interface ICustomerRepository : IDataRepository<Customer>
    {
    }

    public class CustomerRepository : DataRepositoryBase<Customer>, ICustomerRepository
    {

        public CustomerRepository(IRedisContext ctx) : base(ctx)
        {

        }

        //    protected override Customer AddEntity(Hashtable entityContext, Customer entity)
        //    {
        //        var b = entityContext.ContainsKey(entity.Id);

        //        if (!b)
        //        {
        //            entityContext.Add(entity.Id, entity.ToJson());
        //        }

        //        return entity;
        //    }

        //    protected override IEnumerable<Customer> GetEntities(Hashtable entityContext)
        //    {
        //        var customers = new List<Customer>();

        //        foreach (DictionaryEntry item in entityContext)
        //        {
        //            var customer = JsonConvert.DeserializeObject<Customer>((string)item.Value);
        //            customers.Add(customer);
        //        }

        //        return customers;
        //    }

        //    protected override Customer GetEntity(Hashtable entityContext, string id)
        //    {
        //        var b = entityContext.ContainsKey(id);

        //        if (b)
        //        {
        //            var customer = JsonConvert.DeserializeObject<Customer>( (string)entityContext[id]);

        //            return customer;
        //        }

        //        return new Customer()
        //        {
        //            Id = id,
        //            Name = string.Empty,
        //            Surname = string.Empty
        //        };
        //    }

        //    protected override Customer UpdateEntity(Hashtable entityContext, Customer entity)
        //    {
        //        //TODO
        //        return null;
        //    }
        //}
        protected override Customer AddEntity(RedisContext entityContext, Customer entity)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Customer> GetEntities(RedisContext entityContext, string k)
        {
            throw new NotImplementedException();
        }

        protected override Customer GetEntity(RedisContext entityContext, string id)
        {
            var db = entityContext.GetDb();

            string obj = db.StringGet(id);

            var res = JsonConvert.DeserializeObject<Customer>(obj);

            return res;
        }

        protected override Customer Remove(RedisContext entityContext, Customer entity)
        {
            throw new NotImplementedException();
        }

        protected override Customer UpdateEntity(RedisContext entityContext, Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}