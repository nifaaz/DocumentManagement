using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.FrameWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    public class DocumentTypeController : BaseApiController
    {
        [HttpGet]
        public IActionResult GetAllDocumentType()
        {
            DocumentTypeBUS documentTypeBUS = new DocumentTypeBUS();
            var result = documentTypeBUS.GetAllDocumentType();
            return Ok(result);
        }
    }
}