using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.Entity.Role;
using DocumentManagement.Models.Menu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class MenuDAL
    {
        public ReturnResult<Menu> GetMenuByRoleId(Role role)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            var result = new List<Menu>();
            dbProvider.SetQuery("MENU_GET_MENU_BY_ROLE_ID", CommandType.StoredProcedure)
                .SetParameter("RoleId", SqlDbType.Int, role.RoleId, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Menu>(out result)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Menu>()
            {
                ItemList = result,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
        public ReturnResult<Font> FontExport()
        {
            List<Font> fontList = new List<Font>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("FONT_EXPORT", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Font>(out fontList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Font>()
            {
                ItemList = fontList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Font> FontSearch(string searchStr)
        {
            List<Font> fontList = new List<Font>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("FONT_GET_ALL", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Font>(out fontList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Font>()
            {
                ItemList = fontList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Font> GetFontByID(int PhongID)
        {
            List<Font> fontList = new List<Font>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("FONT_GET_BY_ID", CommandType.StoredProcedure)
                .SetParameter("PhongID", SqlDbType.Int, PhongID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Font>(out fontList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Font>()
            {
                ItemList = fontList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Font> GetFontByCoQuanID(int coQuanID)
        {
            List<Font> fontList = new List<Font>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("FONT_GET_BY_COQUANID", CommandType.StoredProcedure)
                .SetParameter("CoQuanID", SqlDbType.Int, coQuanID, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Font>(out fontList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Font>()
            {
                ItemList = fontList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Font> DeleteFont(int PhongID)
        {
            List<Font> fontList = new List<Font>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("FONT_DELETE", CommandType.StoredProcedure)
                .SetParameter("PhongID",SqlDbType.Int,PhongID,ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Font>(out fontList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Font>()
            {
                ItemList = fontList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Font> UpdateFont(Font font)
        {
            List<Font> fontList = new List<Font>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("FONT_EDIT", CommandType.StoredProcedure)
                .SetParameter("PhongID", SqlDbType.Int, font.FontID, ParameterDirection.Input)
                .SetParameter("PhongSo", SqlDbType.NChar, font.FontNumber, 10, ParameterDirection.Input)
                .SetParameter("CoQuanID", SqlDbType.Int, font.OrganID, ParameterDirection.Input)
                .SetParameter("TenPhong", SqlDbType.NVarChar,font.FontName, 50, ParameterDirection.Input)
                .SetParameter("LichSu", SqlDbType.NVarChar, font.History, 500, ParameterDirection.Input)
                .SetParameter("NgonNgu", SqlDbType.NVarChar, font.Lang, 50, ParameterDirection.Input)
                .SetParameter("NgayCapNhat", SqlDbType.DateTime, font.UpdateTime, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Font>(out fontList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Font>()
            {
                ItemList = fontList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public ReturnResult<Font> InsertFont(Font font)
        {
            List<Font> fontList = new List<Font>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            dbProvider.SetQuery("FONT_CREATE", CommandType.StoredProcedure)
                .SetParameter("PhongSo", SqlDbType.NChar, font.FontNumber, 10, ParameterDirection.Input)
                .SetParameter("CoQuanID", SqlDbType.Int, font.OrganID, ParameterDirection.Input)
                .SetParameter("TenPhong", SqlDbType.NVarChar, font.FontName, 50, ParameterDirection.Input)
                .SetParameter("LichSu", SqlDbType.NVarChar, font.History, 500, ParameterDirection.Input)
                .SetParameter("NgonNgu", SqlDbType.NVarChar, font.Lang, 50, ParameterDirection.Input)
                .SetParameter("NgayCapNhat", SqlDbType.DateTime, font.UpdateTime, ParameterDirection.Input)
                .SetParameter("NgayTao", SqlDbType.DateTime, font.CreateTime, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Font>(out fontList)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Font>()
            {
                ItemList = fontList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
    }
}
