using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using backend_core.DTOs.Skill;
using backend_core.Models;

namespace backend_core.DTOs.Category
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        public List<SkillDTO> Skills { get; set; }

        // public DateTime Created_at { get; set; } = DateTime.Now;

    }
}