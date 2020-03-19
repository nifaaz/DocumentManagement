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
    public class NgonNguController : ControllerBase
    {
        private NgonNguBUS NgonNguBUS = NgonNguBUS.GetNgonNguBUSInstance();

        [HttpPost]
        public IActionResult NgonNguGetSearchWithPaging([FromBody] BaseCondition<NgonNgu> condition)
        {
            ReturnResult<NgonNgu> result = NgonNguBUS.NgonNguGetSearchWithPaging(condition);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateNgonNgu(NgonNgu NgonNgu)
        {
            var result = NgonNguBUS.CreateNgonNgu(NgonNgu);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateNgonNgu(NgonNgu NgonNgu)
        {
            var result = NgonNguBUS.EditNgonNgu(NgonNgu);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteNgonNgu([FromQuery] int id)
        {
            return Ok(NgonNguBUS.DeleteNgonNgu(id));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetNgonNguByID(int id)
        {
            return Ok(NgonNguBUS.GetNgonNguByID(id));
        }
    }
}