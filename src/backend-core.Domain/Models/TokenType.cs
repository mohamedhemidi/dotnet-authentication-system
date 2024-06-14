using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Domain.Models
{
    public class TokenType
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
    }
}