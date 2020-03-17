using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Domain
{
    public class SubCategory
    {
        public Guid Id { get; set; }
        public Category Category { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId {get; set;      }
        public string Name { get; set; }
     
    }
}
