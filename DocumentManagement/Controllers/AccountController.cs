using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Common;
using DocumentManagement.Models.Entity.Account;
using DocumentManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Đăng nhập tài khoản
        /// </summary>       
        [HttpPost]
        public IActionResult Login(Account account)
        {
            ReturnResult<Token> loginResult = new ReturnResult<Token>();
            AccountService loginService = new AccountService();
            try
            {
                if (loginService.IsAuthenticate(account))
                {
                    // Create Jwt token for client-side
                    var jwtToken = loginService.CreateToken();
                    loginResult.Item.JwtToken = jwtToken;
                    loginResult.ErrorCode = "0";
                    loginResult.ErrorMessage = "";
                }
                else
                {
                    loginResult.Item.JwtToken = string.Empty;
                    loginResult.ErrorCode = "1";
                    loginResult.ErrorMessage = "";
                }
            }
            catch (Exception ex)
            {
                loginResult.ErrorCode = "1";
                loginResult.ErrorMessage = ex.Message;
            }
            return Ok(loginResult);
        }

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