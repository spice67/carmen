using Common.Core.Contracts;
using Core.Common.Contracts;
using ME.AccntRedis.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Collections.Generic;

namespace ME.AccntRedis.Data.Hash
{
    public abstract class DataRepositoryBase<T, U> : IDataRepository<T>
                 where T : class, new()
         where U : IRedisContext, new()
    {
        private readonly IRedisContext _redisContext;

        public DataRepositoryBase(U ctx)
        {
            _redisContext = ctx;
        }

        protected abstract T AddEntity(U entityContext, T entity);

        protected abstract T UpdateEntity(U entityContext, T entity);

        protected abstract IEnumerable<T> GetEntities(U entityContext, string k);

        protected abstract T GetEntity(U entityContext, string id);

        protected abstract T Remove(U entityContext, T entity);

        public T Add(T entity)
        {
            return AddEntity((U)_redisContext, entity);
        }

        public void Remove(T entity)
        {
            Remove((U)_redisContext, entity);
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public T Update(T entity)
        {
            return UpdateEntity((U)_redisContext, entity);
        }

        public IEnumerable<T> Get(string k)
        {
            return GetEntities((U)_redisContext, k);
        }

        public T GetSingle(string id)
        {
            return GetEntity((U)_redisContext, id);
        }


        //public T Add(T entity)
        //{
        //    return AddEntity(_hashTable, entity);
        //}

        //public IEnumerable<T> Get()
        //{
        //    return GetEntities(_hashTable);
        //}

        //public T Get(string id)
        //{
        //    return GetEntity(_hashTable, id);
        //}

        //public void Remove(int id)
        //{
        //    _hashTable.Remove(id);
        //}

        //public void Remove(T entity)
        //{

        //}

        //public T Update(T entity)
        //{
        //   return  UpdateEntity(_hashTable, entity);
        //}

    }
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, RedisContext> where T : class, new()
    {
        public DataRepositoryBase(IRedisContext ctx) : base((RedisContext)ctx)
        {

        } 
    }
}