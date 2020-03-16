using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adverto.Data;
using Adverto.Domain;
using Microsoft.EntityFrameworkCore;

namespace Adverto.Repositories.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dataContext;
        public AuthRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<User> Login(string name, string password)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(c => c.Name == name);

            if (user == null)
                return null;

            bool result = VerifyPassword(password, user.PasswordHash, user.PasswordSalt);

            if (result == false)
                return null;


            return user;
               

        }


        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);


            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _dataContext.Users.AddAsync(user);
          await _dataContext.SaveChangesAsync();


            return user;

        }

        
        public async Task<bool> UserCheck(string name)
        {
            var isExist = await _dataContext.Users.AnyAsync(c => c.Name == name);

            if (isExist)
                return true;

            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac =new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                        return false;

                }
            }
            return true;
        }
    }
}
