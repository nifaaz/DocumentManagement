using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity.Document
{
    public class DocumentPaging
    {
        public int DocumentId { get; set; }
        /// <summary>
        /// Mã định danh văn bản
        /// </summary>
        public string DocumentCode { get; set; }
        /// <summary>
        /// Số thứ tự văn bản
        /// </summary>
        public int DocOrdinal { get; set; }
        /// <summary>
        /// Tên loại văn bản
        /// </summary>
        public string DocumentType { get; set; }
        /// <summary>
        /// Mã hồ sơ
        /// </summary>
        public int FileId { get; set; }
        /// <summary>
        /// Số của văn bản
        /// </summary>
        public string CodeNumber { get; set; }
        /// <summary>
        /// Ký hiệu của văn bản
        /// </summary>
        public string CodeNotation { get; set; }
        /// <summary>
        /// Ngày, tháng, năm văn bản
        /// </summary>
        public DateTime IssuedDate { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Tên ngôn ngữ
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Số lượng trang văn bản
        /// </summary>
        public int PageAmount { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Ký hiệu thông tin
        /// </summary>
        public string InforSign { get; set; }
        /// <summary>
        /// Từ khóa
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// Chế độ sử dụng
        /// </summary>
        public string Mode { get; set; }
        /// <summary>
        /// Mức độ tin cậy
        /// </summary>
        public int ConfidenceLevel { get; set; }
        /// <summary>
        /// Bút tích
        /// </summary>
        public string Autograph { get; set; }
        /// <summary>
        /// Tình trạng vật lý
        /// </summary>
        public string Format { get; set; }
        public string FileUrl { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
