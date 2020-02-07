using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult GetAllFont()
        {
            var result = fontBUS.GetAllFont();
            return Ok(result);
        }
        [HttpGet]
        public IActionResult Search(string searchStr)
        {
            var result = fontBUS.FontSearch(searchStr);
            return Ok(result);
        }
        [HttpGet("{fontID}")]
        public IActionResult GetFontByID(int fontID)
        {
            var result = fontBUS.GetFontByID(fontID);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetFontByCoQuanID(int CoQuanID)
        {
            var result = fontBUS.GetFontByCoQuanID(CoQuanID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteFont(int FontID)
        {
            var result = fontBUS.DeleteFont(FontID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateFont(Font font)
        {
            Font fontModify = new Font();
            fontModify.FontID = font.FontID;
            fontModify.FontNumber = font.FontNumber;
            fontModify.FontName = font.FontName;
            fontModify.History = font.History;
            fontModify.Lang = font.Lang;
            fontModify.Updated = font.Updated;
            fontModify.OrganID = font.OrganID;
            DateTime currentDate = new DateTime();
            currentDate = DateTime.Now;
            fontModify.UpdateTime = currentDate;
            var result = fontBUS.UpdateFont(fontModify);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult InsertFont(Font font)
        {
            Font fontModify = new Font();
            fontModify.FontNumber = font.FontNumber;
            fontModify.FontName = font.FontName;
            fontModify.History = font.History;
            fontModify.Lang = font.Lang;
            fontModify.Created = font.Created;
            fontModify.OrganID = font.OrganID;
            DateTime currentDate = new DateTime();
            currentDate = DateTime.Now;
            fontModify.CreateTime = currentDate;
            var result = fontBUS.InsertFont(fontModify);
            return Ok(result);
        }
    }
}