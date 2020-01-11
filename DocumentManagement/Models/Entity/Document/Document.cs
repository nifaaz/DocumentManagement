using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity.Document
{
    public class Document
    {
        public int DocumentId { get; set; }
        public int FolderID { get; set; }
        public string DocumentTitle { get; set; }
        /// <summary>
        /// Số văn bản
        /// </summary>
        public string DocumentNumber { get; set; }
        /// <summary>
        /// Số thứ tự văn bản
        /// </summary>
        public float DocumentOrderNum { get; set; }
        public int NumberOfPapers { get; set; }
        public string DocumentContent { get; set; }
        public string DocumentStatusId { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public DateTime PreservationDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int TrustLevelId { get; set; }
        public int FileId { get; set; }
    }
}
