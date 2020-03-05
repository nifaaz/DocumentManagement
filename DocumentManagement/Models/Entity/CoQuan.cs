using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity
{
    public class CoQuan
    {
        public int CoQuanID { get; set; }
        public string TenCoQuan { get; set; }
        public int DiaChiID { get; set; }
        public int TinhID { get; set; }
        public int HuyenID { get; set; }
        public int XaPhuongID { get; set; }
        public int LoaiCoQuanID { get; set; }
        public string CreateBy { get; set; }
        public string UpdatedBy { get; set; }
        public string OrganType { get; set; }
        public string AddressDetail { get; set; }
    }
}
