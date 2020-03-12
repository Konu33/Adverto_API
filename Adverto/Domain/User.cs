using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Surrname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IList<Advert> Adverts { get; set; }
        public IList<Message> Messages { get; set; }
        public bool isAdmin { get; set; }

    }
}
