﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Common.Contracts
{
    public interface IDataRepository
    {

    }

    public interface IDataRepository<T> : IDataRepository
        where T : class, new()
    {
        T Add(T entity);

        void Remove(T entity);

        void Remove(int id);

        T Update(T entity);

        IEnumerable<T> Get(string k);

        T GetSingle(string id);
    }
}
