using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Common.Contracts
{

    public interface IIdentifiableEntity
    {
        long EntityId { get; set; }
    }
}
