using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.Entity.Format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class FormatBUS
    {
        private FormatDAL _formatDAL;
        private FormatDAL FormatDAL
        {
            get
            {
                _formatDAL = new FormatDAL();
                return _formatDAL;
            }
        }
        public ReturnResult<Format> GetAllFormat()
        {
            var result = FormatDAL.GetAllFormat();
            return result;
        }
    }
}
