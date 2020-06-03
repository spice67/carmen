using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Common.Core.Contracts
{
    public interface IRedisContext
    {
        IDatabase GetDb();
    }
}
