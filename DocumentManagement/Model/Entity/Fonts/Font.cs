using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Model.Entity
{
    public class Font
    {
        public int FontID { get; set; }
        public string FontName { get; set; }
        public int FontNumber { get; set; }
        public int AdminiUnitID { get; set; }
        public DateTime CreateTime { get; set; }
        public int Created { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Updated { get; set; }
        public int Status { get; set; }

    }
}
