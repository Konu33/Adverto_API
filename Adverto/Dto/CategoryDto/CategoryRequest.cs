using Adverto.Dto.SubCategoriesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Dto.Category
{
    public class CategoryRequest
    {
        public string Name { get; set; }
        public IEnumerable<SubCategoryRequest> SubCategories { get; set; }
    }
}
