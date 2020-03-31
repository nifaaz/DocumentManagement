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
        public string TabOfContCode { get; set; }
        public string TableOfContName { get; set; }
        public string GearBoxName { get; set; }
        public string GearBoxTitle { get; set; }
        public float Preservationperiod { get; set; }
        public int ProfileCount { get; set; }
        public int NumDoc { get; set; }
        public int DocCount { get; set; }
        public string ProfileCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Note { get; set; }
        public string FontName { get; set; }
        public string OrganName { get; set; }
        public string GearBoxCode { get; set; }
        public int isDeleted { get; set; }
        public int FontID { get; set; }
        public int OrganID { get; set; }
        public DateTime StDate { get; set; }
        public DateTime EDate { get; set; }

        /// <summary>
        /// trạng thái hộp số : nếu = 1 => tất cả tài liệu trong hộp số đã được số hóa
        /// </summary>
        public int Status { get; set; }
    }
}
