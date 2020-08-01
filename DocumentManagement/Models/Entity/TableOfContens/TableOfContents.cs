using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Model.Entity.TableOfContens
{
    public class TableOfContents
    {
        public int TabOfContID { get; set; }
        public string FontCode { get; set; }
        public string TabOfContName { get; set; }
        public string TabOfContNumber { get; set; }
        public string TabOfContCode { get; set; }
        public int StorageID { get; set; }
        public int FontID { get; set; }
        public int OrganID { get; set; }
        public int RepositoryID { get; set; }
        public string CategoryCode { get; set; }
        public string Note { get; set; }
        public string OrganName { get; set; }
        public int GearBoxCount { get; set; }
        public DateTime CreatTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int IsDeleted { get; set; }
    }
}
