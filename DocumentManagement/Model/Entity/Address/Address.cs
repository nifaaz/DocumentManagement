using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Model.Entity.Address
{
    public class Address
    {
        public int AddressID { get; set; }
        public string Detail { get; set; }
        public int ProvincialID { get; set; }
    }
}
