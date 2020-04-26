using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogActivityController : ControllerBase
    {
        private readonly LogActivityBUS logActivityBUS = LogActivityBUS.GetLogActivityBUSInstance;

        [HttpPost]
        public async Task<IActionResult> LogActivityGetSearchWithPaging([FromBody] BaseCondition<LogActivityDTO> condition)
        {
            return Ok(await logActivityBUS.GetSearchLogWithPaging(condition));
        }
    }
}