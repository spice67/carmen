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
    public interface ITransactionRepository : IDataRepository<Transaction>
    {

    }

    public class TransactionRepository : DataRepositoryBase<Transaction>, ITransactionRepository
    {
        protected override Transaction AddEntity(Hashtable entityContext, Transaction entity)
        {
            string transactionObject = entity.ToJson();

            entityContext.Add(entity.Id, transactionObject);

            return entity;
        }

        protected override IEnumerable<Transaction> GetEntities(Hashtable entityContext)
        {
            var transactions = new List<Transaction>();

            var _enum = entityContext.GetEnumerator();
            while (_enum.MoveNext())
            {
                if ( ((string)_enum.Key).StartsWith("TRAN"))
                {
                    var obj = JsonConvert.DeserializeObject<Transaction>((string)_enum.Value);
                    transactions.Add(obj);
                }
            }

            return transactions;
        }

        protected override Transaction GetEntity(Hashtable entityContext, string id)
        {
            throw new NotImplementedException();
        }

        protected override Transaction UpdateEntity(Hashtable entityContext, Transaction entity)
        {
            throw new NotImplementedException();
        }
    }
}