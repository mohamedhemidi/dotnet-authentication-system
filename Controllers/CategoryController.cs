using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_core.Data;
using backend_core.DTOs.Category;
using backend_core.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_core.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ApplicationDBContext _db;
        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _db.categories.ToListAsync();
            var categoriesDTO = categories.Select(s => s.ToCategoryDTO());
            return Ok(categoriesDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await _db.categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.ToCategoryDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequestDTO CategoryDTO)
        {
            var categoryModel = CategoryDTO.ToCategoryCreateDTO();
            await _db.categories.AddAsync(categoryModel);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = categoryModel.Id }, categoryModel.ToCategoryDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryRequestDTO CategoryDTO)
        {
            var Category = await _db.categories.FirstOrDefaultAsync(c => c.Id == id);
            if (Category == null)
            {
                return NotFound();
            }
            Category.Name = CategoryDTO.Name;

            await _db.SaveChangesAsync();
            return Ok(Category.ToCategoryDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var Category = await _db.categories.FirstOrDefaultAsync(c => c.Id == id);
            if (Category == null)
            {
                return NotFound();
            }
            _db.categories.Remove(Category);
            await _db.SaveChangesAsync();
            return NoContent();
        }

    }
}