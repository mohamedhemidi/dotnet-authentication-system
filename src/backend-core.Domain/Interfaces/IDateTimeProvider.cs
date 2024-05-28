using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Domain.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow {get;}
    }
}