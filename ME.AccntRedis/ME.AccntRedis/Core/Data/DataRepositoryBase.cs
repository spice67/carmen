using Core.Common.Contracts;
using ME.AccntRedis.Models;
using System.Collections;
using System.Collections.Generic;

namespace ME.AccntRedis.Data.Hash
{
    public abstract class DataRepositoryBase<T, U> : IDataRepository<T>
                 where T : class, new()
         where U : Hashtable, new()
    {
        private static readonly U _hashTable = new U();

        public static U GetContext()
        {
            return _hashTable;
        }

        public DataRepositoryBase()
        {
            // initialize customers and accounts
            InitializeData();
        }

        private void InitializeData()
        {
            if (_hashTable.Count == 0)
            {
                // accounts
                _hashTable.Add("ACNT0", "CUST12345-67");
                _hashTable.Add("ACNT1", "CUST12346-77");
                _hashTable.Add("ACNT2", "CUST12355-68");
                _hashTable.Add("ACNT3", "CUST12365-87");

                // customer
                var customer = new Customer()
                {
                    Id = "CUST12345-67",
                    Name = "John",
                    Surname = "Doe"
                };

                _hashTable.Add(customer.Id, customer.ToJson());

                customer = new Customer()
                {
                    Id = "CUST12346-77",
                    Name = "Erik",
                    Surname = "Tolentino"
                };

                _hashTable.Add(customer.Id, customer.ToJson());

                customer = new Customer()
                {
                    Id = "CUST12355-68",
                    Name = "Sara",
                    Surname = "Doe"
                };

                _hashTable.Add(customer.Id, customer.ToJson());

                customer = new Customer()
                {
                    Id = "CUST12365-87",
                    Name = "Inez",
                    Surname = "Sarmiento"
                };

                _hashTable.Add(customer.Id, customer.ToJson());

            }

        }

        protected abstract T AddEntity(U entityContext, T entity);

        protected abstract T UpdateEntity(U entityContext, T entity);

        protected abstract IEnumerable<T> GetEntities(U entityContext);

        protected abstract T GetEntity(U entityContext, string id);

        public T Add(T entity)
        {
            return AddEntity(_hashTable, entity);
        }

        public IEnumerable<T> Get()
        {
            return GetEntities(_hashTable);
        }

        public T Get(string id)
        {
            return GetEntity(_hashTable, id);
        }

        public void Remove(int id)
        {
            _hashTable.Remove(id);
        }

        public void Remove(T entity)
        {

        }

        public T Update(T entity)
        {
           return  UpdateEntity(_hashTable, entity);
        }

    }
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, Hashtable> where T : class, new()
    {
    }
}