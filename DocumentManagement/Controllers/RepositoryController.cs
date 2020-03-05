using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Model.Entity.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {
        private static RepositoryBUS repositoryBUS = RepositoryBUS.GetRepositoryBUSInstance;

        [HttpGet]
        public IActionResult GetPagingWithSearchResults(BaseCondition<Repository> condition)
        {
            var result = repositoryBUS.GetPagingWithSearchResults(condition);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetALlRepository()
        {
            var result = repositoryBUS.GetAllRepository();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateRepository(Repository repository)
        {
            var result = repositoryBUS.CreateRepository(repository);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateRepository(Repository repository)
        {
            var result = repositoryBUS.UpdateRepository(repository);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteRepository(int repositoryId)
        {
            var result = repositoryBUS.DeleteRepository(repositoryId);
            return Ok(result);
        }
    }
}