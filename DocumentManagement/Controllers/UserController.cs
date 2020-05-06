using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentManagement.BUS;
using DocumentManagement.Common;
using DocumentManagement.Models.Entity.User;
using Common.Common;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly UserBUS userBUS = UserBUS.GetUserBUSInstance;

        [HttpPost]
        public async Task<IActionResult> UserGetSearchWithPaging ([FromBody] BaseCondition<User> condition)
        {
            ReturnResult<User> result = await userBUS.UserGetSearchWithPaging(condition);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserByID(int id)
        {
            return Ok(userBUS.GetUserByID(id));
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult CreateUser(User user)
        {
            DateTime currentDate = DateTime.Now;
            user.CreateDate = currentDate;
            var result = userBUS.CreateUser(user);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            DateTime currentDate = DateTime.Now;
            user.UpdatedDate = currentDate;
            var result = userBUS.EditUser(user);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteUser([FromQuery] int id)
        {
            return Ok(userBUS.DeleteUser(id));
        }


        //get all role
        [HttpGet]
        public IActionResult GetAllRole()
        {
            try
            {
                RoleBUS roleBUS = new RoleBUS();
                var result = roleBUS.GetAllRole();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //get all user
        [HttpGet]
        public IActionResult GetAllUser()
        {
            try
            {
                var result = userBUS.GetAllUser();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}