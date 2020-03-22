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
    public class ConfidenceLevelController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllConfidenceLevel()
        {
            ConfidenceLevelBUS confidenceLevelBUS = new ConfidenceLevelBUS();
            var result = confidenceLevelBUS.GetAllConfidenceLevel();
            return Ok(result);
        }
    }
}