using Microsoft.AspNetCore.Http;
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
        [Column(TypeName = "decimal(18,4)")]
        public decimal Prize { get; set; }
        public string Description { get; set; }
      
        public string Location { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public string PhotoUrl { get; set; }
        public byte[] ImageByte { get; set; }
    }
}
