using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}