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
    public class FontController : ControllerBase
    {
        public IActionResult GetAllFont()
        {
            FontBUS fontBUS = new FontBUS();
            var result = fontBUS.GetAllFont();
            return Ok(result);
        }
    }
}