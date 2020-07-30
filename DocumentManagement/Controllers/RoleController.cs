using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Common;
using DocumentManagement.FrameWork;
using DocumentManagement.Models.Entity.Account;
using DocumentManagement.Models.Entity.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    public class RoleController : BaseApiController
    {


        [HttpPost]
        public IActionResult UserGroupGetSearchWithPaging([FromBody] BaseCondition<Role> condition)
        {
            RoleBUS roleBUS = new RoleBUS();
            ReturnResult<Role> result = roleBUS.UserGroupGetSearchWithPaging(condition);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetPaging(BaseCondition<Role> condition)
        {
            RoleBUS roleBUS = new RoleBUS();
            var result = roleBUS.GetPaging(condition);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateRole(Role role)
        {
            RoleBUS roleBUS = new RoleBUS();
            var result = roleBUS.CreateRole(role);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateRole(Role role)
        {
            RoleBUS roleBUS = new RoleBUS();
            var result = roleBUS.EditRole(role);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteRole([FromQuery] int id)
        {
            RoleBUS roleBUS = new RoleBUS();
            return Ok(roleBUS.DeleteRole(id));
        }


        [HttpGet]
        public IActionResult GetRolesByUserId(Account account)
        {
            RoleBUS roleBUS = new RoleBUS();
            var result = roleBUS.GetRolesByUserId(account);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRoleByID(int id)
        {
            RoleBUS roleBUS = new RoleBUS();
            return Ok(roleBUS.GetRoleByID(id));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRoleByUserName(string id)
        {
            RoleBUS roleBUS = new RoleBUS();
            return Ok(roleBUS.GetRoleByUserName(id));
        }

        //get all role
        [HttpGet]
        public IActionResult GetAllRole ()
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
    }
}