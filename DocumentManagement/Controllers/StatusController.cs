using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private static CommonStatusBUS commonStatusBUS = CommonStatusBUS.GetCommonStatusBUSInstance;

        [HttpGet]
        public async Task<IActionResult> GetAllStatus()
        {
            var rs = await commonStatusBUS.GetAllStatus();
            return Ok(rs);
        }
    }
}