using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adverto.Data;
using Adverto.Domain;
using Microsoft.EntityFrameworkCore;

namespace Adverto.Repositories
{
    public class AdvertRepo : IAdvertRepo
    {
        private readonly DataContext _context;
        public AdvertRepo(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Advert>> getAdvertsAsync()
        {
            var adverts = await _context.Adverts.ToListAsync();

            return adverts;
        }

        public async Task<Advert> getAdvertAsync(Guid id)
        {
            var advert = await _context.Adverts.SingleOrDefaultAsync(c => c.Id == id);

            return advert;
        }

        public async Task<bool> deleteAdvertAsync(Guid id)
        {
            var advert =await  getAdvertAsync(id);

            if(advert != null)
            {
                _context.Adverts.Remove(advert);

                await _context.SaveChangesAsync();
               
                return true;
            }

            return false;
        }

        public async Task<bool> updateAdvertAsync(Advert advert)
        {
            if (advert == null)
                return false;

             _context.Adverts.Update(advert);

            var result = await _context.SaveChangesAsync();

            return result > 0;

        }

        public async Task<bool> addAdvertAsync(Advert advert)
        {
            if (advert == null)
                return false;

            await _context.Adverts.AddAsync(advert);

            await _context.SaveChangesAsync();


            return true;
                

        }

       
    }
}
