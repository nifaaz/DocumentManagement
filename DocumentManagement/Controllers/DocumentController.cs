using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Models.Entity.Document;
using DocumentManagement.Models.Entity.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllDocument()
        {
            DocumentBUS documentBUS = new DocumentBUS();
            var result = documentBUS.GetAllDocument();
            return Ok(result);
        }
        [HttpGet]
        [Route("{documentId}")]
        public IActionResult GetDocumentById(int documentId)
        {
            DocumentBUS documentBUS = new DocumentBUS();
            var result = documentBUS.GetDocumentById(documentId);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateDocument(Document document)
        {
            DocumentBUS documentBUS = new DocumentBUS();
            var result = documentBUS.CreateDocument(document);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateDocument(Document document)
        {
            DocumentBUS documentBUS = new DocumentBUS();
            var result = documentBUS.UpdateDocument(document);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteDocument(Document document)
        {
            DocumentBUS documentBUS = new DocumentBUS();
            var result = documentBUS.DeleteDocument(document);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult GetPagingWithSearchResults(BaseCondition<DocumentPaging> condition)
        {

            DocumentBUS documentBUS = new DocumentBUS();
            var result = documentBUS.GetPagingWithSearchResults(condition);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetListByProfileId(Profile profile)
        {
            DocumentBUS documentBUS = new DocumentBUS();
            var result = documentBUS.GetListByProfileId(profile);
            return Ok(result);
        }
        
    }
}