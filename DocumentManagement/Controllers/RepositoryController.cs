using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Model.Entity.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetALlRepository()
        {
            RepositoryBUS repositoryBUS = new RepositoryBUS();
            var result = repositoryBUS.GetAllRepository();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateRepository(Repository repository)
        {
            RepositoryBUS repositoryBUS = new RepositoryBUS();
            var result = repositoryBUS.CreateRepository(repository);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateRepository(Repository repository)
        {
            RepositoryBUS repositoryBUS = new RepositoryBUS();
            var result = repositoryBUS.UpdateRepository(repository);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteRepository(int repositoryId)
        {
            RepositoryBUS repositoryBUS = new RepositoryBUS();
            var result = repositoryBUS.DeleteRepository(repositoryId);
            return Ok(result);
        }
    }
}