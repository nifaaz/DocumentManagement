using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Common;
using DocumentManagement.Models.Entity.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MucDoTinCayController : ControllerBase
    {
        private MucDoTinCayBUS mucDoTinCayBUS = MucDoTinCayBUS.GetMucDoTinCayBUSInstance();

        [HttpPost]
        public IActionResult MucDoTinCayGetSearchWithPaging([FromBody] BaseCondition<MucDoTinCay> condition)
        {
            ReturnResult<MucDoTinCay> result = mucDoTinCayBUS.MucDoTinCayGetSearchWithPaging(condition);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateMucDoTinCay(MucDoTinCay MucDoTinCay)
        {
            var result = mucDoTinCayBUS.CreateMucDoTinCay(MucDoTinCay);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateMucDoTinCay(MucDoTinCay MucDoTinCay)
        {
            var result = mucDoTinCayBUS.EditMucDoTinCay(MucDoTinCay);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteMucDoTinCay([FromQuery] int id)
        {
            return Ok(mucDoTinCayBUS.DeleteMucDoTinCay(id));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetMucDoTinCayByID(int id)
        {
            return Ok(mucDoTinCayBUS.GetMucDoTinCayByID(id));
        }
    }
}