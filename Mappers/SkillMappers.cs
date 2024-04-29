using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.DTOs.Skill;
using backend_core.Models;

namespace backend_core.Mappers
{
    public static class SkillMappers
    {
        public static SkillDTO ToSkillDTO(this Skill SkillModel)
        {
            return new SkillDTO
            {
                Id = SkillModel.Id,
                Name = SkillModel.Name,
                CategoryId = SkillModel.CategoryId,
            };
        }
        public static Skill ToSkillCreateDTO(this CreateSkillRequestDTO SkillDTO, int CategoryId)
        {
            return new Skill
            {
                Name = SkillDTO.Name,
                CategoryId = CategoryId
            };
        }
    }
}