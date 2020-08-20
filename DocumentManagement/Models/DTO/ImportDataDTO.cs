using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.DTO
{
    public class ImportDataDTO
    {
        public int Order { get; set; }
        public string Symbol { get; set; }
        public string Date { get; set; }
        public string NameAndCompendium { get; set; }
        public string Author { get; set; }
        public string PageNumber { get; set; }
        public bool Original { get; set; }
        public string Detail { get; set; }
    }
}
