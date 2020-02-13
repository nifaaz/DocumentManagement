using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoQuanController : ControllerBase
    {
        private CoQuanBUS coQuanBUS = CoQuanBUS.GetCoQuanBusInstance();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetCoQuanWithPaging([FromQuery] BaseCondition<CoQuan> condition)
        {
            try
            {
                return Ok(coQuanBUS.GetCoQuanWithPaging(condition));
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        /// <summary>
        /// lấy ra cơ quan theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCoQuanById(int id)
        {
            return Ok(coQuanBUS.GetCoQuanById(id));
        }
        public IActionResult InsertCoQuan(CoQuan cQ)
        {
            CoQuan coQuan = new CoQuan();

            return Ok(coQuanBUS.InssertCoQuan(coQuan));
        }
    }
}