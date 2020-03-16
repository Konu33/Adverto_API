using Adverto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Repositories
{
    public interface IAdvertRepo
    {
       Task<List<Advert>> getAdvertsAsync();
       Task<Advert> getAdvertAsync(Guid id);
        Task<bool> deleteAdvertAsync(Guid id);
        Task<bool> updateAdvertAsync(Advert advert);
        Task<bool> addAdvertAsync(Advert advert);
    }
}
