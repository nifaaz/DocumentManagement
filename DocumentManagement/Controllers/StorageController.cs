using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Model.Entity.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
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
            StorageBUS storageBUS = new StorageBUS();
            var result = storageBUS.GetAllStorage();
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetStorageByID(int storageID)
        {
            StorageBUS storageBUS = new StorageBUS();
            var result = storageBUS.GetStorageByID(storageID);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetStorageByFontID(int fontID)
        {
            StorageBUS storageBUS = new StorageBUS();
            var result = storageBUS.GetStorageByFontID(fontID);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetStorageByRepoID(int repoID)
        {
            StorageBUS storageBUS = new StorageBUS();
            var result = storageBUS.GetStorageByRepoID(repoID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateStorage(Storage storage)
        {
            StorageBUS storageBUS = new StorageBUS();
            var result = storageBUS.CreateStorage(storage);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateStorage(Storage storage)
        {
            StorageBUS storageBUS = new StorageBUS();
            var result = storageBUS.UpdateStorage(storage);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteStorage(int storageId)
        {
            StorageBUS storageBUS = new StorageBUS();
            var result = storageBUS.DeleteStorage(storageId);
            return Ok(result);
        }
    }
}