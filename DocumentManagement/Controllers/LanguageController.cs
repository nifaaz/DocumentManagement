using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.FrameWork;
using LanguageManagement.BUS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    public class LanguageController : BaseApiController
    {
        [HttpGet]
        public IActionResult GetAllLanguage()
        {
            LanguageBUS languageBUS = new LanguageBUS();
            var result = languageBUS.GetAllLanguage();
            return Ok(result);
        }
    }
}