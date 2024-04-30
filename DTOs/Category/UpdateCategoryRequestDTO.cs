using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.DTOs.Category
{
    public class UpdateCategoryRequestDTO
    {
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
    }
}