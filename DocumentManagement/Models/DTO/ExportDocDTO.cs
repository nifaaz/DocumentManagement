using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.DTO
{
    public class ExportDocDTO
    {
        public string DocumentCode { get; set; }
        public string FileCode { get; set; }
        public int DocOrdinal { get; set; }
        public string Subject { get; set; }
        public int PageAmount { get; set; }
        public string Description { get; set; }
        public int Confirmed { get; set; }
        public string TypeName { get; set; }
        public string ConfidenceLevel { get; set; }
        public string Format { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
