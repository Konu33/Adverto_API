using Adverto.Dto.SubCategoriesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Dto.Category
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SubCategoryResponse> SubCategories { get; set; }
    }
}
