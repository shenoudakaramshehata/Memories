﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class paymenturl
    {
        public string status { get; set; }

        public string paymentURL { get; set; }
        public string error_msg { get; set; }
        public string error_code { get; set; }
    }
}
