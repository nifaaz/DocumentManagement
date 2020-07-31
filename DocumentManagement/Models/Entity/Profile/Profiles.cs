using System;
using System.Web;
using System.Net.Http;
using System.ComponentModel;

namespace DocumentManagement.Models.Entity.Profile
{
    public class Profiles
    {
        /// <summary>
        /// mã định danh hồ sơ
        /// </summary>
        public int ProfileId { get; set; }
        /// <summary>
        /// mã hồ sơ lưu trữ
        /// </summary>
        public string FileCode { get; set; }
        /// <summary>
        /// mã cơ quan lưu trữ 
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// mã phông, 
        /// </summary>
        /// tund modified data type of organ id
        public int OrganId { get; set; }
        /// <summary>
        /// mã hộp 
        /// </summary>
        public int GearBoxId { get; set; }
        /// <summary>
        /// mục lục 
        /// </summary>
        public int FileCatalog { get; set; }
        /// <summary>
        /// số hồ sơ 
        /// </summary>
        public string FileNotation { get; set; }

        public string Title { get; set; }

        public string Maintenance { get; set; }
        /// <summary>
        /// chế độ sử dụng
        /// </summary>
        public string Rights { get; set; }

        public string Language { get; set; }
        public int LanguageId { get; set; }
        public int PhysicalStateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// tổng số văn bản trong hồ sơ
        /// </summary>
        public int TotalDoc { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// kí hiệu thông tin
        /// </summary>
        public string InforSign { get; set; }
        /// <summary>
        /// từ khoá
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// số lượng tờ
        /// </summary>
        public int SheetNumber { get; set; }

        public int PageNumber { get; set; }
        public string Format { get; set; }
        public int ProfileTypeId { get; set; }

        public string ProfileTypeName { get; set; }

        public string GearBoxTitle { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public int TotalFiles { get; set; }
        // Hồ sơ ID
        public int ProfileID { get; set; }
        // Hộp số ID
        //public int GearBoxID { get; set; }
        //Mã hồ sơ
        public string ProfileCode { get; set; }
        // Tiêu đề hồ sơ
        public string ProfileTitle { get; set; }
        // Tên hồ sơ
        public string ProfileName { get; set; }
        // Ngày tạo
        public DateTime CreateTime { get; set; }
        // Ngày Cập nhật
        public DateTime UpdateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Note { get; set; }
        public int ShelfLife { get; set; }
        public string GearBoxCode { get; set; }
        public string ProfileNumber { get; set; }
        /// kí hiệu thông tin
        /// </summary>
        public string InfoSign { get; set; }

        public int Status { get; set; }

        public int FontId { get; set; }
        public int TableOfContentId { get; set; }

        public string FontName { get; set; }
        public string OrganName { get; set; }
        public string TableOfContentName { get; set; }
        public string TableOfContentNumber { get; set; }
    }
}