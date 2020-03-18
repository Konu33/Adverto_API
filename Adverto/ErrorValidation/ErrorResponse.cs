using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.ErrorModel
{
    public class ErrorResponse
    {
        public List<ErrorSkeleton> Errors { get; set; } = new List<ErrorSkeleton>();
    }
}
