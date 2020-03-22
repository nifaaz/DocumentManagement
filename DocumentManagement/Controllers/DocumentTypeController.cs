using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
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