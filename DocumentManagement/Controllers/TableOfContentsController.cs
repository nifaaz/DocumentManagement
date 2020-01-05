using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Model.Entity.TableOfContens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableOfContentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllTableOfContents()
        {
            TableOfContentsBUS tableOfContentsBUS = new TableOfContentsBUS();
            var result = tableOfContentsBUS.GetAllTableOfContents();
            return Ok(result);
        }
        [HttpGet]
        public IActionResult Search(string searchStr)
        {
            TableOfContentsBUS tableOfContentsBUS = new TableOfContentsBUS();
            var result = tableOfContentsBUS.TableOfContentsSearch(searchStr);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetTableOfContentsByID(int TableOfContentsID)
        {
            TableOfContentsBUS tableOfContentsBUS = new TableOfContentsBUS();
            var result = tableOfContentsBUS.GetTableOfContentsByID(TableOfContentsID);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetTableOfContentsByStorageID(int storageID)
        {
            TableOfContentsBUS tableOfContentsBUS = new TableOfContentsBUS();
            var result = tableOfContentsBUS.GetTableOfContentsByStorageID(storageID);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetTableOfContentsByFontID(int fontID)
        {
            TableOfContentsBUS tableOfContentsBUS = new TableOfContentsBUS();
            var result = tableOfContentsBUS.GetTableOfContentsByFontID(fontID);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetTableOfContentsByRepositoryID(int repoID)
        {
            TableOfContentsBUS tableOfContentsBUS = new TableOfContentsBUS();
            var result = tableOfContentsBUS.GetTableOfContentsByRepositoryID(repoID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteTableOfContents(int TableOfContentsID)
        {
            TableOfContentsBUS tableOfContentsBUS = new TableOfContentsBUS();
            var result = tableOfContentsBUS.DeleteTableOfContents(TableOfContentsID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateTableOfContents(TableOfContents TableOfContents)
        {
            TableOfContents tableOfContentsModify = new TableOfContents();
            tableOfContentsModify.TabOfContID = TableOfContents.TabOfContID ;
            tableOfContentsModify.TabOfContName = TableOfContents.TabOfContName;
            tableOfContentsModify.TabOfContNumber = TableOfContents.TabOfContNumber;
            tableOfContentsModify.StorageID = TableOfContents.StorageID;
            tableOfContentsModify.RepositoryID = TableOfContents.RepositoryID;
            tableOfContentsModify.FontID = TableOfContents.FontID;
            tableOfContentsModify.CategoryCode = TableOfContents.CategoryCode;
            tableOfContentsModify.Note = TableOfContents.Note;
            DateTime currentDate = new DateTime();
            currentDate = DateTime.Now;
            tableOfContentsModify.UpdateTime = currentDate;
            TableOfContentsBUS tableOfContentsBUS = new TableOfContentsBUS();
            var result = tableOfContentsBUS.UpdateTableOfContents(tableOfContentsModify);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult InsertTableOfContents(TableOfContents TableOfContents)
        {
            TableOfContentsBUS tableOfContentsBUS = new TableOfContentsBUS();
            TableOfContents tableOfContentsModify = new TableOfContents();
            tableOfContentsModify.TabOfContName = TableOfContents.TabOfContName;
            tableOfContentsModify.TabOfContNumber = TableOfContents.TabOfContNumber;
            tableOfContentsModify.StorageID = TableOfContents.StorageID;
            tableOfContentsModify.RepositoryID = TableOfContents.RepositoryID;
            tableOfContentsModify.FontID = TableOfContents.FontID;
            tableOfContentsModify.CategoryCode = TableOfContents.CategoryCode;
            tableOfContentsModify.Note = TableOfContents.Note;
            DateTime currentDate = new DateTime();
            currentDate = DateTime.Now;
            tableOfContentsModify.CreatTime = currentDate;
            var result = tableOfContentsBUS.InsertTableOfContents(tableOfContentsModify);
            return Ok(result);
        }
    }
}