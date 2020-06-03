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