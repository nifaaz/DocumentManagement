using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.DTO
{
    public class LogActivityDTO
    {
        public string UserName { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public string ProfileNumber { get; set; }
        public string CreatorName { get; set; }

        public string UpdatorName { get; set; }

        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
