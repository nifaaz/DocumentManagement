using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Model.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controlleresult
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FontController : ControllerBase
    {

        private FontBUS fontBUS = FontBUS.GetFontBUSInstance;

        [HttpGet]
        public IActionResult GetPagingWithSearchResults([FromBody]BaseCondition<Font> condition)
        {
            try
            {
                var result = fontBUS.GetPagingWithSearchResults(condition);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetFontWithPaging([FromBody] BaseCondition<Font> condition)
        {
            try
            {
                return Ok(fontBUS.GetFontWithPaging(condition));
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpGet]
        public IActionResult GetAllFont()
        {
            try
            {
                 return Ok(fontBUS.GetAllFont());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Search(string searchStr)
        {
            try
            {
                var result = fontBUS.FontSearch(searchStr);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetFontByID(int id)
        {
                return Ok(fontBUS.GetFontByID(id));
        }
        [HttpPost]
        public IActionResult GetFontByCoQuanID([FromBody]BaseCondition<Font> condition)
        {
            try
            {
                var result = fontBUS.GetFontByCoQuanID(condition);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        public IActionResult DeleteFont([FromQuery]int id)
        {
            try
            {
                var result = fontBUS.DeleteFont(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        public IActionResult UpdateFont(Font font)
        {
            DateTime currentDate = DateTime.Now;
            font.UpdateTime = currentDate;
            try
            {
                var result = fontBUS.UpdateFont(font);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult InsertFont(Font font)
        {
            try
            {
                font.IsDeleted = 0;
                DateTime currentDate = DateTime.Now;
                font.CreateTime = currentDate;
                var result = fontBUS.InsertFont(font);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetAllFontSelect2()
        {
            var rs = fontBUS.GetAllFontSelect2();
            return Ok(rs.ItemList);
        }
    }
}