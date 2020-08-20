using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.DTO
{
    public class ValidaImportDto
    {
        public int OrganID { get; set; }
        public int FontID { get; set; }
        public string GearBoxNumber { get; set; }
        public int GearBoxID { get; set; }
        public int ProfileID { get; set; }
        public string ProfileNumber { get; set; }
        public string TableOfContentName { get; set; }
        public string ProfileTitle { get; set; }
        public int TableOfContentID { get; set; }
        public string CreatedBy { get; set; }
        public string UpDatedBy { get; set; }
        public List<ImportDataDTO> ImportDataDTOs { get; set; }
    }
}
