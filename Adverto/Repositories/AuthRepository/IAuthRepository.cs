using Adverto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        Task<User> Login(string name, string password);
        Task<User> Register(User user, string password);
        Task<bool> UserCheck(string name);
    }
}
