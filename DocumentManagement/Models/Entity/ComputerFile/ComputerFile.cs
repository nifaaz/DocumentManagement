using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity.ComputerFile
{
    public class ComputerFile
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// id hồ sơ gắn với file văn bản
        /// </summary>
        public int ProfileId { get; set; }
        public int SheetNumber { get; set; }
        public int PageNumber { get; set; }

        /// <summary>
        /// dung lượng file
        /// </summary>
        public string Size { get; set; }

        public string FolderPath { get; set; }
    }
}
