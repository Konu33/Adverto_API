using Adverto.Dto.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Dto.SubCategoriesDto
{
    public class SubCategoryResponse
    {
        public CategoryResponse Category { get; set; }
      
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
   
    }
}
