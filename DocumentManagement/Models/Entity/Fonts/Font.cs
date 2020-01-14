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
        public string FontNumber { get; set; }
        public int OrganID { get; set; }
        public string OrganName { get; set; }
        public string History { get; set; }
        public string Lang { get; set; }
        public DateTime CreateTime { get; set; }
        public int Created { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Updated { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }

    }
}
