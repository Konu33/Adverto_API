using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public DateTime DateOfSend { get; set; }
        public string Text { get; set; }
    }
}
