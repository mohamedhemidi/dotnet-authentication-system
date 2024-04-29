using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.DTOs.Skill;
using backend_core.Interfaces;
using backend_core.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace backend_core.Controllers
{
    [Route("api/skill")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillRepository _skillRepo;
        private readonly ICategoryRepository _categoryRepo;
        public SkillController(ISkillRepository skillRepo, ICategoryRepository categoryRepo)
        {
            _skillRepo = skillRepo;
            _categoryRepo = categoryRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories =  await _skillRepo.GetAll();
            var categoriesDTO = categories.Select(s => s.ToSkillDTO());
            return Ok(categoriesDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await _skillRepo.Get(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.ToSkillDTO());
        }

        [HttpPost("{CategoryId}")]
        public async Task<IActionResult> Create([FromRoute] int CategoryId, CreateSkillRequestDTO SkillDTO)
        {
            // Check if Category does not exist
            if(!await _categoryRepo.CategoryExists(CategoryId)) {
                return BadRequest("Category does not exist!");
            }
            var skillModel = SkillDTO.ToSkillCreateDTO(CategoryId);
            await _skillRepo.Create(skillModel);
            await _skillRepo.Save();
            return CreatedAtAction(nameof(GetById), new { id = skillModel.Id }, skillModel.ToSkillDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSkillRequestDTO CategoryDTO)
        {
            var Category = await _skillRepo.Get(c => c.Id == id);
            if (Category == null)
            {
                return NotFound();
            }
            Category.Name = CategoryDTO.Name;

            await _skillRepo.Save();
            return Ok(Category.ToSkillDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var Category = await _skillRepo.Get(c => c.Id == id);
            if (Category == null)
            {
                return NotFound();
            }
            _skillRepo.Delete(Category);
            await _skillRepo.Save();
            return NoContent();
        }
        
    }
}