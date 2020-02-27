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
    public class StatisticController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStatisticByNumberOfFiles()
        {
            StatisticBUS documentBUS = new StatisticBUS();
            var result = documentBUS.GetStatisticByNumberOfFiles();
            return Ok(result);
        }
    }
}