using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class MASTERSessionInformation
    {
        public string aes256Key { get; set; }
        public int authenticationLimit { get; set; }
        public string id { get; set; }
        public string updateStatus { get; set; }
        public string version { get; set; }
    }
}
