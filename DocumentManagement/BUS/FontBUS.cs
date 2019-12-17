using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class FontBUS
    {
        private FontDAL _fontDAL;
        private FontDAL FontDAL
        {
            get
            {
                _fontDAL = new FontDAL();
                return _fontDAL;
            }
        }
        public ReturnResult<Font> GetAllFont()
        {
            var result = FontDAL.GetAllFont();
            return result;
        }
    }
}
