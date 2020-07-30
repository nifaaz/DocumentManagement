using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Common;
using DocumentManagement.FrameWork;
using DocumentManagement.Models.Entity.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    public class LoaiVanBanController : BaseApiController
    {
        private LoaiVanBanBUS loaiVanBanBUS = LoaiVanBanBUS.GetLoaiVanBanBUSInstance();

        [HttpPost]
        public IActionResult LoaiVanBanGetSearchWithPaging([FromBody] BaseCondition<LoaiVanBan> condition)
        {
            ReturnResult<LoaiVanBan> result = loaiVanBanBUS.LoaiVanBanGetSearchWithPaging(condition);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateLoaiVanBan(LoaiVanBan LoaiVanBan)
        {
            var result = loaiVanBanBUS.CreateLoaiVanBan(LoaiVanBan);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateLoaiVanBan(LoaiVanBan LoaiVanBan)
        {
            var result = loaiVanBanBUS.EditLoaiVanBan(LoaiVanBan);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteLoaiVanBan([FromQuery] int id)
        {
            return Ok(loaiVanBanBUS.DeleteLoaiVanBan(id));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetLoaiVanBanByID(int id)
        {
            return Ok(loaiVanBanBUS.GetLoaiVanBanByID(id));
        }
    }
}