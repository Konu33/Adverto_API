using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Dto.AdvertDto
{
    public class FileUPloadApi
    {
        public IFormFile file { get; set; }
    }
}
