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
    public class TinhTrangVatLyController : ControllerBase
    {
        private TinhTrangVatLyBUS tinhTranhVatLyBUS = TinhTrangVatLyBUS.GetTinhTrangVatLyBUSInstance();

        [HttpPost]
        public IActionResult TinhTrangVatLyGetSearchWithPaging([FromBody] BaseCondition<TinhTrangVatLy> condition)
        {
            ReturnResult<TinhTrangVatLy> result = tinhTranhVatLyBUS.TinhTrangVatLyGetSearchWithPaging(condition);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateTinhTrangVatLy(TinhTrangVatLy TinhTrangVatLy)
        {
            var result = tinhTranhVatLyBUS.CreateTinhTrangVatLy(TinhTrangVatLy);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateTinhTrangVatLy(TinhTrangVatLy TinhTrangVatLy)
        {
            var result = tinhTranhVatLyBUS.EditTinhTrangVatLy(TinhTrangVatLy);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteTinhTrangVatLy([FromQuery] int id)
        {
            return Ok(tinhTranhVatLyBUS.DeleteTinhTrangVatLy(id));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTinhTrangVatLyByID(int id)
        {
            return Ok(tinhTranhVatLyBUS.GetTinhTrangVatLyByID(id));
        }
    }
}