using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.DTO
{
    public class ExportProfileDTO
    {
        public int ProfileId { get; set; }
        public string Title { get; set; }
        public string Maintenance { get; set; }
        public string Description { get; set; }
        public string ProfileTypeName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string GearBoxCode { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
