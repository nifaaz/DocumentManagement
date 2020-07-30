using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Common;
using DocumentManagement.FrameWork;
using DocumentManagement.Models.Entity.Account;
using DocumentManagement.Models.Entity.User;
using DocumentManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    public class AccountController : BaseApiController
    {
        [HttpPost]
        public IActionResult CreateAccount(Account account)
        {
            AccountService accountService = new AccountService();
            var hasedPassword = accountService.CreateHashedPassword(account.Password);

            // Assign a new hashed password to account
            account.Password = hasedPassword;
            AccountBUS accountBUS = new AccountBUS();
            var result = accountBUS.CreateAccount(account);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteAccount(Account account)
        {
            AccountBUS accountBUS = new AccountBUS();
            var result = accountBUS.DeleteAccount(account);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult EditPassword(Account account)
        {
            AccountBUS accountBUS = new AccountBUS();
            var result = accountBUS.EditPassword(account);
            return Ok(result);
        }

    }
}