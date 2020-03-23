using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageManagement.BUS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LanguageController : ControllerBase
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