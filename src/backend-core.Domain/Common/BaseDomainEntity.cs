using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Domain.Common
{
    public abstract class BaseDomainEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Updated_at { get; set; }
        public Guid Created_by { get; set; }
        public Guid Updated_by { get; set; }
    }
}