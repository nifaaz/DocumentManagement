using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity.ComputerFile
{
    public class ComputerFile
    {
        public string FileName { get; set; }
        public string Url { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// id hồ sơ gắn với file văn bản
        /// </summary>
        public int profileId { get; set; }
        public int SheetNumber { get; set; }
        public int PageNumber { get; set; }
    }
}
