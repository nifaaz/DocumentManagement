using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Common;
using DocumentManagement.Models.Entity.Account;
using DocumentManagement.Models.Entity.User;
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
        public IActionResult Login([FromBody] Account account)
        {
            ReturnResult<User> loginResult;
            AccountService loginService = new AccountService();
            try
            {
                if (loginService.IsAuthenticate(account))
                {
                    // Create Jwt token for client-side
                    var jwtToken = loginService.CreateToken();
                    AccountBUS accountBusiness = new AccountBUS();
                    var quser = accountBusiness.GetUserByUserName(account);
                    var user = new User()
                    {
                        Status = quser.Item.Status,
                        RoleName = quser.Item.RoleName,
                        UserRole = quser.Item.UserRole,
                        UserName = account.UserName,
                        Token = new Token()
                        {
                            JwtToken = jwtToken.JwtToken,
                            Expiration = jwtToken.Expiration
                        }
                    };
                    loginResult = new ReturnResult<User>()
                    {
                        Item = user,
                        ErrorCode = "0",
                        ErrorMessage = ""
                    };
                }
                else
                {
                    loginResult = new ReturnResult<User>()
                    {
                        IsSuccess = false,
                        ErrorCode = "-1",
                        ErrorMessage = "Tài khoản hoặc mật khẩu không chính xác, vui lòng thử lại.",
                    };
                }
            }
            catch (Exception ex)
            {
                loginResult = new ReturnResult<User>()
                {
                    IsSuccess = false,
                    ErrorCode = "1",
                    ErrorMessage = ex.Message
                };
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