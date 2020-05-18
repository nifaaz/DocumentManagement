using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.DTO
{
    public class DataStatisticsDTO
    {
        public string FontName { get; set; }
        public int Status { get; set; }
        public string TableOfNumber { get; set; }
        public string GearBoxCode { get; set; }
        public string ProfileCode { get; set; }
        public string FileName { get; set; }
        public string DocNumber { get; set; }
        public int ComputerFileID { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
