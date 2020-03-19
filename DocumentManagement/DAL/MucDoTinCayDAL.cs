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
    public class MucDoTinCayDAL
    {
        private MucDoTinCayDAL() { }

        private static volatile MucDoTinCayDAL _instance;

        static object key = new object();

        public static MucDoTinCayDAL GetMucDoTinCayDALInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new MucDoTinCayDAL();
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


        public ReturnResult<MucDoTinCay> MucDoTinCayGetSearchWithPaging(BaseCondition<MucDoTinCay> condition)
        {
            DbProvider provider = new DbProvider();
            List<MucDoTinCay> list = new List<MucDoTinCay>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            var result = new ReturnResult<MucDoTinCay>();
            try
            {
                provider.SetQuery("MucDoTinCay_GET_SEARCH_WITH_PAGING", System.Data.CommandType.StoredProcedure)
                    .SetParameter("InWhere", System.Data.SqlDbType.NVarChar, condition.IN_WHERE ?? String.Empty)
                    .SetParameter("InSort", System.Data.SqlDbType.NVarChar, condition.IN_SORT ?? String.Empty)
                    .SetParameter("StartRow", System.Data.SqlDbType.Int, condition.PageIndex)
                    .SetParameter("PageSize", System.Data.SqlDbType.Int, condition.PageSize)
                    .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output).GetList<MucDoTinCay>(out list).Complete();

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
        public ReturnResult<MucDoTinCay> GetPaging(BaseCondition<MucDoTinCay> condition)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            dbProvider.SetQuery("MucDoTinCay_GET_PAGING", CommandType.StoredProcedure)
                .SetParameter("FromRecord", SqlDbType.NVarChar, condition.FromRecord, 50, ParameterDirection.Input)
                .SetParameter("PageSize", SqlDbType.NVarChar, condition.PageSize, 50, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .ExcuteNonQuery()
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<MucDoTinCay>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
        public ReturnResult<MucDoTinCay> CreateMucDoTinCay(MucDoTinCay MucDoTinCay)
        {

            DbProvider provider = new DbProvider();
            var result = new ReturnResult<MucDoTinCay>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            try
            {
                provider.SetQuery("MucDoTinCay_CREATE", System.Data.CommandType.StoredProcedure)
                    .SetParameter("LoaiMucDoTinCay", SqlDbType.NVarChar, MucDoTinCay.LoaiMucDoTinCay, 50, ParameterDirection.Input)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output)
                    .GetSingle<MucDoTinCay>(out MucDoTinCay).Complete();

                provider.GetOutValue("ErrorCode", out outCode)
                          .GetOutValue("ErrorMessage", out outMessage);

                if (outCode != "0" || outCode == "")
                {
                    result.ErrorCode = outCode;
                    result.ErrorMessage = outMessage;
                }
                else
                {
                    result.Item = MucDoTinCay;
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

        public ReturnResult<MucDoTinCay> EditMucDoTinCay(MucDoTinCay MucDoTinCay)
        {
            ReturnResult<MucDoTinCay> result;
            DbProvider db;
            try
            {
                result = new ReturnResult<MucDoTinCay>();
                db = new DbProvider();
                db.SetQuery("MucDoTinCay_EDIT", CommandType.StoredProcedure)
                    .SetParameter("MucDoTinCayID", SqlDbType.Int, MucDoTinCay.MucDoTinCayID, ParameterDirection.Input)
                    .SetParameter("LoaiMucDoTinCay", SqlDbType.NVarChar, MucDoTinCay.LoaiMucDoTinCay, 50, ParameterDirection.Input)
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

        public ReturnResult<MucDoTinCay> DeleteMucDoTinCay(int id)
        {
            DbProvider provider = new DbProvider();
            var result = new ReturnResult<MucDoTinCay>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            MucDoTinCay item = new MucDoTinCay();
            try
            {
                provider.SetQuery("MucDoTinCay_DELETE", CommandType.StoredProcedure)
                     .SetParameter("MucDoTinCayID", SqlDbType.Int, id, ParameterDirection.Input)
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

        public ReturnResult<MucDoTinCay> GetMucDoTinCayByID(int MucDoTinCayId)
        {
            var result = new ReturnResult<MucDoTinCay>();
            MucDoTinCay item = new MucDoTinCay();
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            int totalRows = 0;
            try
            {
                dbProvider.SetQuery("MucDoTinCay_GET_BY_ID", CommandType.StoredProcedure)
               .SetParameter("MucDoTinCayID", SqlDbType.Int, MucDoTinCayId, ParameterDirection.Input)
               .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
               .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
               .GetSingle<MucDoTinCay>(out item)
               .Complete();

            }
            catch (Exception ex)
            {

                throw;
            }
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<MucDoTinCay>()
            {
                Item = item,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
                TotalRows = totalRows
            };
        }
    }
}
