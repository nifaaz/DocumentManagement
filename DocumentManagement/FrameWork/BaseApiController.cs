using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.FrameWork
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}