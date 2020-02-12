using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.Common;
using DocumentManagement.Models.Entity.User;
using DocumentManagement.DAL;
using DocumentManagement.Model;
using System.Data;

namespace DocumentManagement.DAL
{
    public class UserDAL
    {
        private UserDAL() { }
        private static volatile UserDAL _instance;
        static object key = new object();

        public static UserDAL GetUserDALInstance
        {
            get
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new UserDAL();
                    }
                }

                return _instance;
            }
            
            private set
            {
                _instance = value;
            }
        }

        public ReturnResult<User> GetSearchUserWithPaging (BaseCondition<User> condition)
        {
            DbProvider dbProvider = new DbProvider();
            var result = new ReturnResult<User>();
            List<User> users = new List<User>();
            try
            {
                dbProvider.SetQuery("USER_GET_SEARCH_WITH_PAGING", CommandType.StoredProcedure)
                .SetParameter("PageIndex", SqlDbType.Int, condition.PageIndex)
                .SetParameter("PageSize", SqlDbType.Int, condition.PageSize)
                .SetParameter("InWhere", SqlDbType.VarChar, condition.IN_WHERE ?? string.Empty, 200)
                .SetParameter("InSort", SqlDbType.VarChar, condition.IN_SORT ?? string.Empty, 200)
                .SetParameter("TotalRows", SqlDbType.Int, DBNull.Value, ParameterDirection.Output)
                .SetParameter("ErrorCode", SqlDbType.Int, DBNull.Value, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetList<User>(out users)
                .Complete();

                // get output value
                dbProvider.GetOutValue("ErrorCode", out string errorCode)
                    .GetOutValue("ErrorMessage", out string errorMessage)
                    .GetOutValue("TotalRows", out int totalRows);

                if (int.Parse(errorCode) != 0)
                {
                    result.ErrorCode = errorCode;
                    result.ErrorMessage = errorMessage;
                    return result;
                }
                if (users != null)
                {
                    result.ItemList = users;
                    result.TotalRows = totalRows;
                    result.ErrorCode = "0";
                    result.ErrorMessage = "";
                }
                
            }
            catch (Exception ex)
            {
                result.ErrorCode = "-1";
                result.ErrorMessage = ex.Message;
            }
            
            return result;
        }
    }
}
