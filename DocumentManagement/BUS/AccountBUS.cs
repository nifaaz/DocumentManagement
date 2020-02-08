using DocumentManagement.Common;
using DocumentManagement.DAL;
using DocumentManagement.Models.Entity.Account;
using DocumentManagement.Models.Entity.User;
using DocumentManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.BUS
{
    public class AccountBUS
    {
        private AccountDAL _accountDAL;
        private AccountDAL AccountDAL
        {
            get
            {
                _accountDAL = new AccountDAL();
                return _accountDAL;
            }
        }
        public ReturnResult<User> GetUserByUserName(Account login)
        {
            return AccountDAL.GetUserByUserName(login);
        }

        public ReturnResult<Account> CreateAccount(Account account)
        {
            return AccountDAL.CreateAccount(account);
        }

        public ReturnResult<Account> DeleteAccount(Account account)
        {
            return AccountDAL.DeleteAccount(account);
        }

        public ReturnResult<Account> EditPassword(Account account)
        {
            var result = new ReturnResult<Account>();
            AccountService accountService = new AccountService();
            if (accountService.IsAuthenticate(account))
            {
                var hasedPassword = accountService.CreateHashedPassword(account.Password);

                // Assign a new hashed password to account
                account.Password = hasedPassword;
                result = AccountDAL.EditPassword(account);
            }
            else
            {
                result.ErrorCode = "1";
                result.ErrorMessage = "Nhập sai mật khẩu hiện tại";
            }
            return AccountDAL.EditPassword(account);
        }

        
    }
}
