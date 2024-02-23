using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class Session
    {
        public string merchant { get; set; }
        public string result { get; set; }
        public string checkoutMode { get; set; }
        public string successIndicator { get; set; }
        public SessionInformation SessionInformation { get; set; }
    }
}
