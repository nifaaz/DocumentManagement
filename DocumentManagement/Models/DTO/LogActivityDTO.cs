using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.DTO
{
    public class LogActivityDTO
    {
        public int DocumentID { get; set; }
        public string DocumentCode { get; set; }
        public int LogID { get; set; }
        public int DocOrdinal { get; set; }
        public string Description { get; set; }
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
