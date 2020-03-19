using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.Entity.Category;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class TinhTrangVatLyDAL
    {
        private TinhTrangVatLyDAL() { }

        private static volatile TinhTrangVatLyDAL _instance;

        static object key = new object();

        public static TinhTrangVatLyDAL GetTinhTrangVatLyDALInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new TinhTrangVatLyDAL();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }

        //


        public ReturnResult<TinhTrangVatLy> TinhTrangVatLyGetSearchWithPaging(BaseCondition<TinhTrangVatLy> condition)
        {
            DbProvider provider = new DbProvider();
            List<TinhTrangVatLy> list = new List<TinhTrangVatLy>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            var result = new ReturnResult<TinhTrangVatLy>();
            try
            {
                provider.SetQuery("TinhTrangVatLy_GET_SEARCH_WITH_PAGING", System.Data.CommandType.StoredProcedure)
                    .SetParameter("InWhere", System.Data.SqlDbType.NVarChar, condition.IN_WHERE ?? String.Empty)
                    .SetParameter("InSort", System.Data.SqlDbType.NVarChar, condition.IN_SORT ?? String.Empty)
                    .SetParameter("StartRow", System.Data.SqlDbType.Int, condition.PageIndex)
                    .SetParameter("PageSize", System.Data.SqlDbType.Int, condition.PageSize)
                    .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output).GetList<TinhTrangVatLy>(out list).Complete();

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
        public ReturnResult<TinhTrangVatLy> GetPaging(BaseCondition<TinhTrangVatLy> condition)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            dbProvider.SetQuery("TinhTrangVatLy_GET_PAGING", CommandType.StoredProcedure)
                .SetParameter("FromRecord", SqlDbType.NVarChar, condition.FromRecord, 50, ParameterDirection.Input)
                .SetParameter("PageSize", SqlDbType.NVarChar, condition.PageSize, 50, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .ExcuteNonQuery()
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<TinhTrangVatLy>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
        public ReturnResult<TinhTrangVatLy> CreateTinhTrangVatLy(TinhTrangVatLy TinhTrangVatLy)
        {

            DbProvider provider = new DbProvider();
            var result = new ReturnResult<TinhTrangVatLy>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            try
            {
                provider.SetQuery("TinhTrangVatLy_CREATE", System.Data.CommandType.StoredProcedure)
                    .SetParameter("TinhTrang", SqlDbType.NVarChar, TinhTrangVatLy.TinhTrang, 50, ParameterDirection.Input)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output)
                    .GetSingle<TinhTrangVatLy>(out TinhTrangVatLy).Complete();

                provider.GetOutValue("ErrorCode", out outCode)
                          .GetOutValue("ErrorMessage", out outMessage);

                if (outCode != "0" || outCode == "")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
                else
                {
                    result.Item = TinhTrangVatLy;
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

        public ReturnResult<TinhTrangVatLy> EditTinhTrangVatLy(TinhTrangVatLy TinhTrangVatLy)
        {
            ReturnResult<TinhTrangVatLy> result;
            DbProvider db;
            try
            {
                result = new ReturnResult<TinhTrangVatLy>();
                db = new DbProvider();
                db.SetQuery("TinhTrangVatLy_EDIT", CommandType.StoredProcedure)
                    .SetParameter("TinhTrangVatLyID", SqlDbType.Int, TinhTrangVatLy.TinhTrangVatLyId, ParameterDirection.Input)
                    .SetParameter("TinhTrang", SqlDbType.NVarChar, TinhTrangVatLy.TinhTrang, 50, ParameterDirection.Input)
                    .SetParameter("ErrorCode", SqlDbType.Int, DBNull.Value, ParameterDirection.Output)
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

        public ReturnResult<TinhTrangVatLy> DeleteTinhTrangVatLy(int id)
        {
            DbProvider provider = new DbProvider();
            var result = new ReturnResult<TinhTrangVatLy>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            TinhTrangVatLy item = new TinhTrangVatLy();
            try
            {
                provider.SetQuery("TinhTrangVatLy_DELETE", CommandType.StoredProcedure)
                     .SetParameter("TinhTrangVatLyID", SqlDbType.Int, id, ParameterDirection.Input)
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

        public ReturnResult<TinhTrangVatLy> GetTinhTrangVatLyByID(int TinhTrangVatLyId)
        {
            var result = new ReturnResult<TinhTrangVatLy>();
            TinhTrangVatLy item = new TinhTrangVatLy();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            try
            {
                dbProvider.SetQuery("TinhTrangVatLy_GET_BY_ID", CommandType.StoredProcedure)
               .SetParameter("TinhTrangVatLyID", SqlDbType.Int, TinhTrangVatLyId, ParameterDirection.Input)
               .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
               .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
               .GetSingle<TinhTrangVatLy>(out item)
               .Complete();

            }
            catch (Exception ex)
            {

                throw;
            }
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<TinhTrangVatLy>()
            {
                Item = item,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
    }
}
