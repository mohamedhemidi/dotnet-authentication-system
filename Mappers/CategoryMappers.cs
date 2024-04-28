using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.DTOs.Category;
using backend_core.Models;

namespace backend_core.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDTO ToCategoryDTO(this Category CategoryModel)
        {
            return new CategoryDTO
            {
                Id = CategoryModel.Id,
                Name = CategoryModel.Name,
                Skills = CategoryModel.Skills.Select(s => s.ToSkillDTO()).ToList()
            };
        }
        public static Category ToCategoryCreateDTO(this CreateCategoryRequestDTO CategoryDTO)
        {
            return new Category
            {
                Name = CategoryDTO.Name,
            };
        }
    }
}