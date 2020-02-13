using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrganTypeController : ControllerBase
    {

        private LoaiCoQuanBUS loaiCoQuanBUS = LoaiCoQuanBUS.GetLoaiCoQuanBUSInstance();

        [HttpGet]
        public IActionResult GetALLLoaiCoQuan()
        {
            return Ok(loaiCoQuanBUS.GetALLLoaiCoQuan());
        }
    }
}