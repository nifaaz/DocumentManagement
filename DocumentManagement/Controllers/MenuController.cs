using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Models.Entity.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        public IActionResult GetMenuByRoleId(Role role)
        {
            MenuBUS menuBUS = new MenuBUS();
            var result = menuBUS.GetMenuByRoleId(role);
            return Ok(result);
        }
    }
}