using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class MasterSession
    {
        public string merchant { get; set; }
        public string result { get; set; }
        
        public MASTERSessionInformation SessionInformation { get; set; }
    }
}
