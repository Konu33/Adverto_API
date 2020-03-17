using Adverto.Domain;
using Adverto.Dto.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Dto.AdvertDto
{
    public class AdvertResponse
    {
        public string Name { get; set; }
        public decimal Prize { get; set; }
        public string Description { get; set; }

        public string Location { get; set; }
        public virtual CategoryResponse Category { get; set; }
      
        public Guid CategoryId { get; set; }
       

    }
}
