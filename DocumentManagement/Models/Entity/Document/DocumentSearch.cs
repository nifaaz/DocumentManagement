using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity.Document
{

    // entity cho tra cuu van ban
    public class DocumentSearch
    {
        public int OrganID { get; set; }
        public int FontID { get; set; }
        public int TableOfContentID { get; set; }
        public int GearBoxID { get; set; }
        public int ProfileID { get; set; }
        /// <summary>
        /// Ký hiệu của văn bản
        /// </summary>
        public string CodeNotation { get; set; }
        /// <summary>
        /// Ngày, tháng, năm văn bản
        /// </summary>
        public DateTime IssuedDate { get; set; }
        public int DocumentTypeId { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Subject { get; set; }
    }
}
