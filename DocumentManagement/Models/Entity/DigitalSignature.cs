using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity
{
    public class DigitalSignature
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string Path { get; set; }

        public string Base64String { get; set; } = "";
        /// <summary>
        /// lưu lại trạng thái
        /// </summary>
        public int Status { get; set; }

        public string ServerPath { get; set; }
    }
}
