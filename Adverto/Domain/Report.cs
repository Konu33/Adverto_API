using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Domain
{
    public class Report
    {
        public Guid Id { get; set; }
        public bool IsCheckedByAdmin { get; set; }
        public string Description { get; set; }
        public Advert Advert { get; set; }
        [ForeignKey("Advert")]
        public Guid AdvertId { get; set; }
    }
}
