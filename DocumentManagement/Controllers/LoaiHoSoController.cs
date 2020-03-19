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
    public class LoaiHoSoController : ControllerBase
    {
        private LoaiHoSoBUS loaiHoSoBUS = LoaiHoSoBUS.GetLoaiHoSoBUSInstance();

        [HttpPost]
        public IActionResult LoaiHoSoGetSearchWithPaging([FromBody] BaseCondition<LoaiHoSo> condition)
        {
            ReturnResult<LoaiHoSo> result = loaiHoSoBUS.LoaiHoSoGetSearchWithPaging(condition);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateLoaiHoSo(LoaiHoSo LoaiHoSo)
        {
            var result = loaiHoSoBUS.CreateLoaiHoSo(LoaiHoSo);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateLoaiHoSo(LoaiHoSo LoaiHoSo)
        {
            var result = loaiHoSoBUS.EditLoaiHoSo(LoaiHoSo);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteLoaiHoSo([FromQuery] int id)
        {
            return Ok(loaiHoSoBUS.DeleteLoaiHoSo(id));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetLoaiHoSoByID(int id)
        {
            return Ok(loaiHoSoBUS.GetLoaiHoSoByID(id));
        }
    }
}