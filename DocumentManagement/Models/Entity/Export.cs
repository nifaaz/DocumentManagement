using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity
{
    public class Export
    {
        public string FontName { get; set; }
        public string TableOfName { get; set; }
        public string GearBoxCode { get; set; }
        public string ProfileCode { get; set; }
        public string FileName { get; set; }
        public DateTime updateDate { get; set; }
    }
}
