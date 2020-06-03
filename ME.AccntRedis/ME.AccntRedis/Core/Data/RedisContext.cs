using ME.AccntRedis.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Core.Contracts
{
    public class RedisContext : IRedisContext
    {
        private readonly IDatabase _redisDb;

        private static bool initialized;

        public RedisContext()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

            _redisDb = redis.GetDatabase();

            if (!initialized)
            {
                InitializeData();
                initialized = true;
            }
        }

        public IDatabase GetDb()
        {
            return _redisDb;
        }

        private void InitializeData()
        {

            //accounts
            _redisDb.StringSet("ACCNT:CUST12345-67", "ACCNT0");
            _redisDb.StringSet("ACCNT:CUST12346-77", "ACCNT1");
            _redisDb.StringSet("ACCNT:CUST12355-68", "ACCNT2");
            _redisDb.StringSet("ACCNT:CUST12365-87", "ACCNT3");


            // customer
            var customer = new Customer()
            {
                Id = "CUST12345-67",
                Name = "John",
                Surname = "Doe"
            };

            _redisDb.StringSet(customer.Id, customer.ToJson());

            customer = new Customer()
            {
                Id = "CUST12346-77",
                Name = "Erik",
                Surname = "Tolentino"
            };

            _redisDb.StringSet(customer.Id, customer.ToJson());

            customer = new Customer()
            {
                Id = "CUST12355-68",
                Name = "Sara",
                Surname = "Doe"
            };

            _redisDb.StringSet(customer.Id, customer.ToJson());

            customer = new Customer()
            {
                Id = "CUST12365-87",
                Name = "Inez",
                Surname = "Sarmiento"
            };

            _redisDb.StringSet(customer.Id, customer.ToJson());

        }

    }
}
