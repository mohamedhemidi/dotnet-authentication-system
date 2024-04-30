using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Helpers
{
    public class QueryObject
    {
        public string? Name { get; set; } = null;

        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
    }
}