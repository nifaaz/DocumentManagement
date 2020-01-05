using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Model.Entity.GearBox
{
    public class GearBox
    {
        public int GearBoxID { get; set; }
        public int TabOfContID { get; set; }
        public string GearBoxName { get; set; }
        public string GearBoxTitle { get; set; }
        public string Preservationperiod { get; set; }
        public int ProfileCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
