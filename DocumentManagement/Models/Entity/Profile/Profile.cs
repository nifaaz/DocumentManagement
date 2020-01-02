using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity.Profile
{
    public class Profile
    {
        // Hồ sơ ID
        public int ProfileId { get; set; }
        // Hộp số ID
        public int GearBoxId { get; set; }
        // Tiêu đề hồ sơ
        public string ProfileTitle { get; set; }
        // Tên hồ sơ
        public string ProfileName { get; set; }
        // Số lượng văn bản
        public int NumberOfDocs { get; set; }
        // Ngày tạo
        public DateTime CreatedDate { get; set; }
        // Ngày udpate
        public DateTime UpdatedDate { get; set; }

    }
}
