using DocumentManagement.Common;
using DocumentManagement.Model;
using DocumentManagement.Models.Entity.Account;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.DAL
{
    public class AccountDAL
    {
        public ReturnResult<Account> GetUserByUserName(Account account)
        {
            DbProvider dbProvider = new DbProvider();
            string outCode = String.Empty;
            string outMessage = String.Empty;
            Account result = new Account();
            dbProvider.SetQuery("ACCOUNT_GET_USER_BY_USER_NAME", CommandType.StoredProcedure)
                .SetParameter("UserName", SqlDbType.NVarChar, account.UserName, 50, ParameterDirection.Input)
                .SetParameter("ErrorCode", SqlDbType.NVarChar, DBNull.Value, 100, ParameterDirection.Output)
                .SetParameter("ErrorMessage", SqlDbType.NVarChar, DBNull.Value, 4000, ParameterDirection.Output)
                .GetSingle<Account>(out result)
                .Complete();
            dbProvider.GetOutValue("ErrorCode", out outCode)
                       .GetOutValue("ErrorMessage", out outMessage);

            return new ReturnResult<Account>()
            {
                Item = result,
                ErrorCode = outCode,
                ErrorMessage = outMessage,
            };
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
