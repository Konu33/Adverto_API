using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adverto.Data;
using Adverto.Domain;
using Microsoft.EntityFrameworkCore;

namespace Adverto.Repositories.CategoryRepository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly DataContext _context;
        public CategoryRepo(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> addCategoryAsync(Category category)
        {
            //sprawdzenie unikalnosci categori

            var isValidName = await _context.Categories.AnyAsync(c => c.Name == category.Name);
            if(!isValidName)
            {
                await _context.Categories.AddAsync(category);
                int result = await _context.SaveChangesAsync();
                return result > 0;

            }
            return false;
        }

        public async Task<List<Category>> getCategoriesAsync()
        {
            return  await _context.Categories.Include(c=>c.SubCategories).ToListAsync();
        }

        public async Task<Category> getCategoryAsync(Guid id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);

            if(category != null)
            {
                return category;
            }

            

             return null;
           
        }

        public async Task<bool> removeCategoryAsync(Guid categoryId)
        {

            var category = await getCategoryAsync(categoryId);

            if(category != null)
            {
                _context.Categories.Remove(category);
             int result =   await _context.SaveChangesAsync();
                return result > 0;
            }


            return false;
        }

        public async Task<bool> updateCategoryAsync(Category category)
        {
             _context.Categories.Update(category);

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
