using Adverto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Repositories.SubCategoryRepo
{
    public interface ISubCatergoryRepo
    {
       Task<bool> removeSubCategoryAsync(Guid id);
        Task<bool> addSubCategoryAsync(SubCategory subcategory);
        Task<bool> updateSubCategoryAsync(SubCategory subcategory);
        Task<SubCategory> getSubCategoryAsync(Guid id);
        Task<List<SubCategory>> getSubCategoriesAsync();
    }
}
