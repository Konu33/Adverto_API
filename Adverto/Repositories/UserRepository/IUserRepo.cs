using Adverto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Repositories
{
    public interface IUserRepo
    {
        Task<bool> addUserAsync(User user);
        Task<bool> removeUserAsync(Guid id);
        Task<bool> updateUserAsync(User user);
        Task<User> getUser(Guid id);
        Task<List<User>> getUsers();
    }
}
