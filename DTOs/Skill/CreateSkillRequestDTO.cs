using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.DTOs.Skill
{
    public class CreateSkillRequestDTO
    {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
    }
}