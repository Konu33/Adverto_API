using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Domain
{
    public class Advert
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Prize { get; set; }
        public string Description { get; set; }
      
        public string Location { get; set; }
        public virtual Category Category { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }

    }
}
