using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adverto.Data;
using Adverto.Domain;
using Microsoft.EntityFrameworkCore;

namespace Adverto.Repositories.UserRepository
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _dataContext;
        public UserRepo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> addUserAsync(User user)
        {
            if (user == null)
                return false;

            var userToCreate = new User()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };

            await _dataContext.Users.AddAsync(userToCreate);

            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<User> getUser(Guid id)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(c => c.Id == id);

            return user;
        }

        public async Task<List<User>> getUsers()
        {
            return (await _dataContext.Users.Include(c=>c.Adverts).ToListAsync());
        }

        public async Task<bool> removeUserAsync(Guid id)
        {

            var user = await _dataContext.Users.SingleOrDefaultAsync(c => c.Id == id);

            _dataContext.Users.Remove(user);

            var delete = await _dataContext.SaveChangesAsync();

            return delete > 0;
        }

        public async Task<bool> updateUserAsync(User user)
        {
            _dataContext.Users.Update(user);
            var update = await _dataContext.SaveChangesAsync();

            return update > 0;
        }
    }
}
