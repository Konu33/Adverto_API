using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adverto.Data;
using Adverto.Domain;
using Microsoft.EntityFrameworkCore;

namespace Adverto.Repositories.SubCategoryRepo
{
    public class SubCategoryRepo : ISubCatergoryRepo
    {
        private readonly DataContext _context;
        public SubCategoryRepo(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> addSubCategoryAsync(SubCategory subcategory)
        {
            await _context.SubCategories.AddAsync(subcategory);
            int result = await _context.SaveChangesAsync();


            return result > 0;
        }

        public async Task<List<SubCategory>> getSubCategoriesAsync()
        {
            return (await _context.SubCategories.ToListAsync());
        }

        public async Task<SubCategory> getSubCategoryAsync(Guid id)
        {

            var subcategory = await _context.SubCategories.SingleOrDefaultAsync(c => c.Id == id);

            return subcategory;

        }

        public async Task<bool> removeSubCategoryAsync(Guid id)
        {
            var subcategory = await _context.SubCategories.SingleOrDefaultAsync(c => c.Id == id);

            if(subcategory!=null)
            {
                _context.SubCategories.Remove(subcategory);
                int result = await _context.SaveChangesAsync();

                return result > 0;
            }
            return false;
        }

        public async Task<bool> updateSubCategoryAsync(SubCategory subcategory)
        {
            _context.SubCategories.Update(subcategory);

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
