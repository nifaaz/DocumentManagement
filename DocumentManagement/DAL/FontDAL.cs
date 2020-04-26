using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Model.Entity;
using DocumentManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class FontDAL
    {
        private FontDAL() { }

        private static volatile FontDAL _instance;

        static object key = new object();

        public static FontDAL GetFontDALInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new FontDAL();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        public async Task<ReturnResult<Font>> GetFontWithPaging(BaseCondition<Font> condition)
        {
            DbProvider provider = new DbProvider();
            List<Font> list = new List<Font>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            var result = new ReturnResult<Font>();
            try
            {
                provider.SetQuery("FONT_GET_PAGING", System.Data.CommandType.StoredProcedure)
                    .SetParameter("InWhere", System.Data.SqlDbType.NVarChar, condition.IN_WHERE ?? String.Empty)
                    .SetParameter("InSort", System.Data.SqlDbType.NVarChar, condition.IN_SORT ?? String.Empty)
                    .SetParameter("StartRow", System.Data.SqlDbType.Int, condition.PageIndex)
                    .SetParameter("PageSize", System.Data.SqlDbType.Int, condition.PageSize)
                    .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output).GetList<Font>(out list).Complete();

                if (list.Count > 0)
                {
                    result.ItemList = list;
                }
                provider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage)
                           .GetOutValue("TotalRecords", out string totalRows);

                if (outCode != "0")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
                else
                {
                    result.ErrorCode = "";
                    result.ErrorMessage = "";
                    result.TotalRows = int.Parse(totalRows);
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public ReturnResult<Font> GetPagingWithSearchResults(BaseCondition<Font> condition)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            try
            {
                dbProvider.SetQuery("FONT_GET_PAGING", CommandType.StoredProcedure)
                .SetParameter("FromRecord", SqlDbType.NVarChar, condition.FromRecord, ParameterDirection.Input)
                .SetParameter("PageSize", SqlDbType.NVarChar, condition.PageSize, ParameterDirection.Input)
                .SetParameter("InWhere", SqlDbType.NVarChar, condition.IN_WHERE, ParameterDirection.Input)
                .SetParameter("InSort", SqlDbType.NVarChar, condition.IN_SORT, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .ExcuteNonQuery()
                .Complete();
            }
            catch (Exception)
            {

                throw;
            }
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Font>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
        public async Task<ReturnResult<FontDTO>> GetAllFont()
        {
            List<FontDTO> fontList = new List<FontDTO>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            try
            {
                dbProvider.SetQuery("FONT_GET_ALL", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<FontDTO>(out fontList)
                .Complete();
            }
            catch (Exception)
            {

                throw;
            }
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<FontDTO>()
            {
                ItemList = fontList,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
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
        public async Task<ReturnResult<Font>> GetFontByID(int PhongID)
        {
            var result = new ReturnResult<Font>();
            Font item = new Font();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            try
            {
                dbProvider.SetQuery("FONT_GET_BY_ID", CommandType.StoredProcedure)
               .SetParameter("PhongID", SqlDbType.Int, PhongID, ParameterDirection.Input)
               .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
               .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
               .GetSingle<Font>(out item)
               .Complete();

            }
            catch (Exception ex)
            {

                throw;
            }
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Font>()
            {
                Item = item,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
        public async Task<ReturnResult<Font>> GetFontByCoQuanID(BaseCondition<Font> condition)
        {
            List<Font> fonts = new List<Font>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            var result = new ReturnResult<Font>();
            try
            {
                dbProvider.SetQuery("FONT_GET_BY_COQUANID", CommandType.StoredProcedure)
                .SetParameter("InWhere", System.Data.SqlDbType.NVarChar, condition.IN_WHERE == null ? "" : condition.IN_WHERE)
                .SetParameter("InSort", System.Data.SqlDbType.NVarChar, condition.IN_SORT == null ? "" : condition.IN_SORT)
                .SetParameter("StartRow", System.Data.SqlDbType.Int, condition.PageIndex)
                .SetParameter("PageSize", System.Data.SqlDbType.Int, condition.PageSize)
                .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<Font>(out fonts)
                .Complete();

                if (fonts.Count > 0)
                {
                    result.ItemList = fonts;
                }
                dbProvider.GetOutValue("ErrorCode", out outCode)
                           .GetOutValue("ErrorMessage", out outMessage)
                           .GetOutValue("TotalRecords", out string totalRows);

                if (outCode != "0")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
                else
                {
                    result.ErrorCode = "";
                    result.ErrorMessage = "";
                    result.TotalRows = int.Parse(totalRows);
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public ReturnResult<Font> DeleteFont(int PhongID)
        {
            DbProvider provider = new DbProvider();
            var result = new ReturnResult<Font>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            Font item = new Font();
            try
            {
                provider.SetQuery("FONT_DELETE", CommandType.StoredProcedure)
                     .SetParameter("PhongID", SqlDbType.Int, PhongID, ParameterDirection.Input)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output)
                    .ExcuteNonQuery().Complete();

                provider.GetOutValue("ErrorCode", out outCode)
                          .GetOutValue("ErrorMessage", out outMessage);

                if (outCode != "0")
                {
                    result.Failed(outCode, outMessage);
                }
                else
                {
                    result.ErrorCode = "0";
                    result.ErrorMessage = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public ReturnResult<Font> UpdateFont(Font font)
        {
            ReturnResult<Font> result;
            DbProvider db;
            try
            {
                result = new ReturnResult<Font>();
                db = new DbProvider();
                db.SetQuery("FONT_EDIT", CommandType.StoredProcedure)
                    .SetParameter("PhongID", SqlDbType.Int, font.FontID, ParameterDirection.Input)
                    .SetParameter("PhongSo", SqlDbType.NChar, font.FontNumber, 50, ParameterDirection.Input)
                    .SetParameter("CoQuanID", SqlDbType.Int, font.OrganID, ParameterDirection.Input)
                    .SetParameter("NgonNguID", SqlDbType.Int, font.LanguageId, ParameterDirection.Input)
                    .SetParameter("TenPhong", SqlDbType.NVarChar, font.FontName, 50, ParameterDirection.Input)
                    .SetParameter("CongCu", SqlDbType.NVarChar, font.LookupTools, 50, ParameterDirection.Input)
                    .SetParameter("LichSu", SqlDbType.NVarChar, font.History, 500, ParameterDirection.Input)
                    .SetParameter("NgonNgu", SqlDbType.NVarChar, font.Lang, 50, ParameterDirection.Input)
                    .SetParameter("NgayCapNhat", SqlDbType.NVarChar, font.UpdateTime.ToString(), 50, ParameterDirection.Input)
                    .SetParameter("GhiChu", SqlDbType.NVarChar, font.Note, 300, ParameterDirection.Input)
                    .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                    .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .ExcuteNonQuery()
                    .Complete();
                db.GetOutValue("ErrorCode", out string errorCode)
                    .GetOutValue("ErrorMessage", out string errorMessage);
                if (errorCode.ToString() == "0")
                {
                    result.ErrorCode = "0";
                    result.ErrorMessage = "";
                }
                else
                {
                    result.Failed(errorCode, errorMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public ReturnResult<Font> InsertFont(Font font)
        {

            DbProvider provider = new DbProvider();
            var result = new ReturnResult<Font>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            try
            {
                provider.SetQuery("[FONT_CREATE]", System.Data.CommandType.StoredProcedure)
                .SetParameter("PhongSo", SqlDbType.NVarChar, font.FontNumber, 10, ParameterDirection.Input)
                .SetParameter("CoQuanID", SqlDbType.Int, font.OrganID, ParameterDirection.Input)
                .SetParameter("NgonNguID", SqlDbType.Int, font.LanguageId, ParameterDirection.Input)
                .SetParameter("TenPhong", SqlDbType.NVarChar, font.FontName, 50, ParameterDirection.Input)
                .SetParameter("CongCu", SqlDbType.NVarChar, font.LookupTools, 50, ParameterDirection.Input)
                .SetParameter("LichSu", SqlDbType.NVarChar, font.History, 500, ParameterDirection.Input)
                .SetParameter("GhiChu", SqlDbType.NVarChar, font.Note, 300, ParameterDirection.Input)
                .SetParameter("NgayTao", SqlDbType.NVarChar, font.CreateTime.ToString(),100,ParameterDirection.Input)
                .SetParameter("isDeleted", SqlDbType.Int , font.IsDeleted,ParameterDirection.Input)
                .SetParameter("NgonNgu", SqlDbType.NVarChar, font.Lang, 50, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                    .GetSingle<Font>(out font).Complete();

                provider.GetOutValue("ErrorCode", out outCode)
                          .GetOutValue("ErrorMessage", out outMessage);

                if (outCode != "0" || outCode == "")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
                else
                {
                    result.Item = font;
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        //
        public async Task<ReturnResult<FontSelect2>> GetAllFontSelect2()
        {
            List<FontSelect2> fontSelect2s = new List<FontSelect2>();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            try
            {
                dbProvider.SetQuery("FONT_GET_ALL_GETIDANDNAME_SELECT2", CommandType.StoredProcedure)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<FontSelect2>(out fontSelect2s)
                .Complete();
            }
            catch (Exception)
            {

                throw;
            }
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<FontSelect2>()
            {
                ItemList = fontSelect2s,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
    }
}