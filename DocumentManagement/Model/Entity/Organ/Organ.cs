using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Model.Entity.Organ
{
    public class Organ
    {
        public int OrganID { get; set; }
        public int AddressID { get; set; }
        public string OrganName { get; set; }
        public int OrganTypeID { get; set; }
        public DateTime CreateTime { get; set; }
        public int Created { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Updated { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
        public bool? Deleted { get; set; }
    }
}
