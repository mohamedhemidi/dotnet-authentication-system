using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.DTOs.Skill
{
    public class SkillDTO
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        public Guid CategoryId { get; set; }

    }
}