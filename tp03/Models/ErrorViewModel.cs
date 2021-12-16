using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tp03.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
