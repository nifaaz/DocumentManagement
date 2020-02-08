using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Model.Entity.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private static StorageBUS storageBUS = StorageBUS.GetStorageBUSInstance;

        [HttpGet]
        public IActionResult GetPagingWithSearchResults(BaseCondition<Storage> condition)
        {
            var result = storageBUS.GetPagingWithSearchResults(condition);
            return Ok(result);
        }

        [HttpGet]
        //[HttpGet]
        //public IActionResult SearchStorage(string searchStr)
        //{
        //    StorageBUS storageBUS = new StorageBUS();
        //    var result = storageBUS.StorageSearch(searchStr);
        //    return Ok(result);
        //}
        [HttpGet]
        public IActionResult GetALlStorage()
        {
            var result = storageBUS.GetAllStorage();
            return Ok(result);
        }
        [HttpGet("{storageID}")]
        public IActionResult GetStorageByID(int storageID)
        {
            var result = storageBUS.GetStorageByID(storageID);
            return Ok(result);
        }
        [HttpGet("{fontID}")]
        public IActionResult GetStorageByFontID(int fontID)
        {
            var result = storageBUS.GetStorageByFontID(fontID);
            return Ok(result);
        }
        [HttpGet("{repoID}")]
        public IActionResult GetStorageByRepoID(int repoID)
        {
            var result = storageBUS.GetStorageByRepoID(repoID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateStorage(Storage storage)
        {
            var result = storageBUS.CreateStorage(storage);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateStorage(Storage storage)
        {
            var result = storageBUS.UpdateStorage(storage);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteStorage(int storageId)
        {
            var result = storageBUS.DeleteStorage(storageId);
            return Ok(result);
        }
    }
}