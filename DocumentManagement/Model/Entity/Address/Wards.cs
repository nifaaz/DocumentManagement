using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Model.Entity.Address
{
    public class Wards
    {
        public int WardsID { get; set; }
        public int DistrictID { get; set; }
        public string WardsName { get; set; }
        public String Level { get; set; }
    }
}
