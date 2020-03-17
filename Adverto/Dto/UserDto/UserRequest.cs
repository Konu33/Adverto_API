﻿using Adverto.Dto.AdvertDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Dto.UserDto
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Surrname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool isAdmin { get; set; }
        public IList<AdvertRequest> Adverts { get; set; }
    }
}
