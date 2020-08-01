using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.DTO
{
    public class ProfileDTO
    {
        public int OrganID { get; set; }
        public int FontID { get; set; }
        public int TableOfContID { get; set; }
        public int GearBoxID { get; set; }
        public int ProfileID { get; set; }
        public string ProfileCode { get; set; }
    }
}
