using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.DTO
{
    public class OrganDTO
    {
        public string TenCoQuan { get; set; }
        public int OrganID { get; set; }
    }

    public class OrganSelect2
    {
        public string Text { get; set; }
        public int Id { get; set; }
    }
}
