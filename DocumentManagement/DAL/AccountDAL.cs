using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.Entity.Account;
using DocumentManagement.Models.Entity.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class AccountDAL
    {
        public ReturnResult<User> GetUserByUserName(Account account)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            ReturnResult<User> result = new ReturnResult<User>();
            User user = new User();
            dbProvider.SetQuery("ACCOUNT_GET_USER_BY_USER_NAME", CommandType.StoredProcedure)
                .SetParameter("UserName", SqlDbType.NVarChar, account.UserName, 50, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetSingle<User>(out user)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            if (!string.IsNullOrEmpty(outMessage))
            {
                result.Item = null;
                result.ErrorCode = outCode;
                result.ErrorMessage = outMessage;
            }
            else
            {
                result.Item = user;
                result.ErrorCode = "0";
                result.ErrorMessage = "";
                
            }
            return result;
        }

        public ReturnResult<Account> EditPassword(Account account)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            Account result = new Account();
            dbProvider.SetQuery("ACCOUNT_EDIT_PASSWORD", CommandType.StoredProcedure)
                .SetParameter("UserName", SqlDbType.NVarChar, account.UserName, 50, ParameterDirection.Input)
                .SetParameter("Password", SqlDbType.NVarChar, account.Password, 800, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .ExcuteNonQuery()
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);
            return new ReturnResult<Account>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }

        

        public ReturnResult<Account> DeleteAccount(Account account)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            Account result = new Account();
            dbProvider.SetQuery("ACCOUNT_DELETE", CommandType.StoredProcedure)
                .SetParameter("UserName", SqlDbType.NVarChar, account.UserName, 50, ParameterDirection.Input)
                .SetParameter("Password", SqlDbType.NVarChar, account.Password, 800, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .ExcuteNonQuery()
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);
            return new ReturnResult<Account>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }

        public ReturnResult<Account> CreateAccount(Account account)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            Account result = new Account();
            dbProvider.SetQuery("ACCOUNT_CREATE", CommandType.StoredProcedure)
                .SetParameter("UserName", SqlDbType.NVarChar, account.UserName, 50, ParameterDirection.Input)
                .SetParameter("Password", SqlDbType.NVarChar, account.Password, 800, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .ExcuteNonQuery()
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);
            return new ReturnResult<Account>()
            {
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
        }
    }
}
