using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Model.Entity.Address
{
    public class District
    {
        public int DistrictID { get; set; }
        public int ProvincialID { get; set; }
        public string DistrictName { get; set; }
        public string Level { get; set; }
    }
}
