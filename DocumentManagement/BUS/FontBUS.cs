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
        public ReturnResult<Font> FontExport()
        {
            var result = FontDAL.FontExport();
            return result;
        }
        public ReturnResult<Font> GetFontByID(int fontID)
        {
            var rs = FontDAL.GetFontByID(fontID);
            return rs;
        }
        public ReturnResult<Font> GetFontByCoQuanID(int coQuanID)
        {
            var rs = FontDAL.GetFontByCoQuanID(coQuanID);
            return rs;
        }
        public ReturnResult<Font> FontSearch(string searchStr)
        {
            var rs = FontDAL.FontSearch(searchStr);
            return rs;
        }
        public ReturnResult<Font> DeleteFont(int fontID)
        {
            var rs = FontDAL.DeleteFont(fontID);
            return rs;
        }
        public ReturnResult<Font> UpdateFont(Font font)
        {
            var rs = FontDAL.UpdateFont(font);
            return rs;
        }
        public ReturnResult<Font> InsertFont(Font font)
        {
            var rs = FontDAL.InsertFont(font);
            return rs;
        }
    }
}
