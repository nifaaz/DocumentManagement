using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class LogActivityDAL
    {
        private LogActivityDAL() { }
        private static volatile LogActivityDAL _instance;
        static object key = new object();

        public static LogActivityDAL GetLogActivityDALInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new LogActivityDAL();
                    }
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }

        public ReturnResult<LogActivityDTO> GetSearchLogWithPaging(BaseCondition<LogActivityDTO> condition)
        {
            DbProvider provider = new DbProvider();
            List<LogActivityDTO> list = new List<LogActivityDTO>();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            string totalRecords = String.Empty;
            var result = new ReturnResult<LogActivityDTO>();
            try
            {
                provider.SetQuery("GET_LOG_OF_USER_ACTIVITY", System.Data.CommandType.StoredProcedure)
                    .SetParameter("InWhere", System.Data.SqlDbType.NVarChar, condition.IN_WHERE ?? String.Empty)
                    .SetParameter("InSort", System.Data.SqlDbType.NVarChar, condition.IN_SORT ?? String.Empty)
                    .SetParameter("StartRow", System.Data.SqlDbType.Int, condition.PageIndex)
                    .SetParameter("PageSize", System.Data.SqlDbType.Int, condition.PageSize)
                    .SetParameter("TotalRecords", System.Data.SqlDbType.Int, DBNull.Value, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorCode", System.Data.SqlDbType.NVarChar, DBNull.Value, 100, System.Data.ParameterDirection.Output)
                    .SetParameter("ErrorMessage", System.Data.SqlDbType.NVarChar, DBNull.Value, 4000, System.Data.ParameterDirection.Output).GetList<LogActivityDTO>(out list).Complete();

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
    }
}
