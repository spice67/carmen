﻿using ME.AccntRedis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Newtonsoft.Json;
using Core.Common.Contracts;
using ME.AccntRedis.Data.Hash;
using Common.Core.Contracts;
using StackExchange.Redis;

namespace ME.AccntRedis.Data
{
    public interface ITransactionRepository : IDataRepository<Transaction>
    {

    }

    public class TransactionRepository : DataRepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(IRedisContext ctx) : base(ctx)
        {

        }

         protected override Transaction AddEntity(RedisContext entityContext, Transaction entity)
        {
            string transactionObj = entity.ToJson();
            entityContext.GetDb().HashSet(entity.AccountNo, entity.Id, transactionObj);

            return entity;
        }

        protected override IEnumerable<Transaction> GetEntities(RedisContext entityContext, string k)
        {
            var trans = entityContext.GetDb().HashGetAll(k);

            List<Transaction> lt = new List<Transaction>();
            foreach (var item in trans)
            {
                lt.Add(JsonConvert.DeserializeObject<Transaction>(item.Value));
            }

            return lt;

        }

        protected override Transaction GetEntity(RedisContext entityContext, string id)
        {
            throw new NotImplementedException();
        }

        protected override Transaction Remove(RedisContext entityContext, Transaction entity)
        {
            throw new NotImplementedException();
        }

        protected override Transaction UpdateEntity(RedisContext entityContext, Transaction entity)
        {
            throw new NotImplementedException();
        }

    }
}