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
        [HttpGet]
        public IActionResult GetAllFont()
        {
            FontBUS fontBUS = new FontBUS();
            var result = fontBUS.GetAllFont();
            return Ok(result);
        }
        [HttpGet]
        public IActionResult Search(string searchStr)
        {
            FontBUS fontBUS = new FontBUS();
            var result = fontBUS.FontSearch(searchStr);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetFontByID(int fontID)
        {
            FontBUS fontBUS = new FontBUS();
            var result = fontBUS.GetFontByID(fontID);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetFontByCoQuanID(int CoQuanID)
        {
            FontBUS fontBUS = new FontBUS();
            var result = fontBUS.GetFontByCoQuanID(CoQuanID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteFont(int FontID)
        {
            FontBUS fontBUS = new FontBUS();
            var result = fontBUS.DeleteFont(FontID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateFont(Font font)
        {
            FontBUS fontBUS = new FontBUS();
            var result = fontBUS.UpdateFont(font);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult InsertFont(Font font)
        {
            FontBUS fontBUS = new FontBUS();
            var result = fontBUS.InsertFont(font);
            return Ok(result);
        }
    }
}