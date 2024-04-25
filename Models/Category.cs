using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public List<Skill>? Skills { get; set; } = new List<Skill>();
    }
}