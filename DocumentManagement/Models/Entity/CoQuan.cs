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
        public int LoaiCoQuan { get; set; }
    }
}
