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
    public class FormatController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllFormat()
        {
            FormatBUS formatBUS = new FormatBUS();
            var result = formatBUS.GetAllFormat();
            return Ok(result);
        }
    }
}