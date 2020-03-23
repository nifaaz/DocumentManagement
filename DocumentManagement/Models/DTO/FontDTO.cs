using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models.DTO
{
    public class FontDTO
    {
        public string FontName { get; set; }
        public int FontID { get; set; }
    }

    public class FontSelect2
    {
        public string Text { get; set; }
        public int Id { get; set; }
    }
}
