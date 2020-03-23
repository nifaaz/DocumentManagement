using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Model.Entity.TableOfContens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TableOfContentsController : ControllerBase
    {
        private static TableOfContentsBUS tableOfContentsBUS = TableOfContentsBUS.GetTableOfContentsBUSInstance;

        [HttpPost]
        public IActionResult GetPagingWithSearchResults([FromBody]BaseCondition<TableOfContents> condition)
        {
            var result = tableOfContentsBUS.GetPagingWithSearchResults(condition);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllTableOfContents()
        {
            var result = tableOfContentsBUS.GetAllTableOfContents();
            return Ok(result);
        }
        [HttpGet]
        public IActionResult Search(string searchStr)
        {
            var result = tableOfContentsBUS.TableOfContentsSearch(searchStr);
            return Ok(result);
        }
        [HttpGet("{tableOfContentsID}")]
        public IActionResult GetTableOfContentsByID(int tableOfContentsID)
        {
            var result = tableOfContentsBUS.GetTableOfContentsByID(tableOfContentsID);
            return Ok(result);
        }
        [HttpGet("{storageID}")]
        public IActionResult GetTableOfContentsByStorageID(int storageID)
        {
            var result = tableOfContentsBUS.GetTableOfContentsByStorageID(storageID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult GetTableOfContentsByFontID([FromBody]BaseCondition<TableOfContents> condition)
        {
            var result = tableOfContentsBUS.GetTableOfContentsByFontID(condition);
            return Ok(result);
        }
        [HttpGet("{repoID}")]
        public IActionResult GetTableOfContentsByRepositoryID(int repoID)
        {
            var result = tableOfContentsBUS.GetTableOfContentsByRepositoryID(repoID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteTableOfContents([FromQuery]int id)
        {
            var result = tableOfContentsBUS.DeleteTableOfContents(id);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateTableOfContents([FromBody]TableOfContents TableOfContents)
        {
            TableOfContents tableOfContentsModify = new TableOfContents();
            tableOfContentsModify.TabOfContID = TableOfContents.TabOfContID ;
            tableOfContentsModify.TabOfContName = TableOfContents.TabOfContName;
            tableOfContentsModify.TabOfContNumber = TableOfContents.TabOfContNumber;
            tableOfContentsModify.StorageID = TableOfContents.StorageID;
            tableOfContentsModify.RepositoryID = TableOfContents.RepositoryID;
            tableOfContentsModify.FontID = TableOfContents.FontID;
            tableOfContentsModify.TabOfContCode = TableOfContents.TabOfContCode;
            tableOfContentsModify.TabOfContNumber = TableOfContents.TabOfContNumber;
            tableOfContentsModify.Note = TableOfContents.Note;
            DateTime currentDate = new DateTime();
            currentDate = DateTime.Now;
            tableOfContentsModify.UpdateTime = currentDate;
            var result = tableOfContentsBUS.UpdateTableOfContents(tableOfContentsModify);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult InsertTableOfContents([FromBody]TableOfContents TableOfContents)
        {
            TableOfContents tableOfContentsModify = new TableOfContents();
            tableOfContentsModify.TabOfContName = TableOfContents.TabOfContName;
            tableOfContentsModify.TabOfContNumber = TableOfContents.TabOfContNumber;
            tableOfContentsModify.StorageID = TableOfContents.StorageID;
            tableOfContentsModify.RepositoryID = TableOfContents.RepositoryID;
            tableOfContentsModify.FontID = TableOfContents.FontID;
            tableOfContentsModify.TabOfContCode = TableOfContents.TabOfContCode;
            tableOfContentsModify.TabOfContNumber = TableOfContents.TabOfContNumber;
            tableOfContentsModify.Note = TableOfContents.Note;
            DateTime currentDate = new DateTime();
            currentDate = DateTime.Now;
            tableOfContentsModify.CreatTime = currentDate;
            tableOfContentsModify.IsDeleted = 0;
            var result = tableOfContentsBUS.InsertTableOfContents(tableOfContentsModify);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllTabSelect2()
        {
            var rs = tableOfContentsBUS.GetAllTabSelect2();
            return Ok(rs.ItemList);
        }
    }
}