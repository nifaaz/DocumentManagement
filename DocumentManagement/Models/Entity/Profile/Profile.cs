using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity.Profile
{
    public class Profiles
    {
        // Hồ sơ ID
        public int ProfileID { get; set; }
        // Hộp số ID
        public int GearBoxID { get; set; }
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
        public int ShelfLife {get;set;}
        public string ProfileTypeName { get; set; }
        public string GearBoxCode { get; set; }
        public string ProfileNumber { get; set; }
        /// <summary>
        /// mã hồ sơ
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
        public string OrganId { get; set; }
        /// <summary>
        /// mã hộp 
        /// </summary>
        public string GearBoxId { get; set; }
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
        public string InfoSign { get; set; }
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


        public string GearBoxTitle { get; set; }

    }
}
