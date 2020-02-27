using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.Entity.Statistic
{
    public class Statistic
    {
        public int DocumentId { get; set; }
        public int NumberOfFiles { get; set; }
        public int NumberOfDocuments { get; set; }
    }
}
