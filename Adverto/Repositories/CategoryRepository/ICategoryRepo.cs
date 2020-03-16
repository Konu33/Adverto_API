using Adverto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Repositories.CategoryRepository
{
    public interface ICategoryRepo
    {
        Task<bool> addCategoryAsync(Category category);
        Task<bool> removeCategoryAsync(Guid categoryId);
        Task<Category> getCategoryAsync(Guid id);
        Task<List<Category>> getCategoriesAsync();
        Task<bool> updateCategoryAsync(Category category);
    }
}
