﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<SubCategory> SubCategories { get; set; }

    }
}
