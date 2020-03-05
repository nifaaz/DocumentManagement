using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Model.Entity.Organ;
using DocumentManagement.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrganController : ControllerBase
    {
        private static OrganBUS organBUS = OrganBUS.GetOrganBUSInstance;

        [HttpGet]
        public IActionResult GetPagingWithSearchResults(BaseCondition<Organ> condition)
        {
            var result = organBUS.GetPagingWithSearchResults(condition);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllOrgan()
        {
            var rs = organBUS.GetAllOrgan();
            return Ok(rs);
        }
        [HttpGet]
        public IActionResult Search(string searchStr)
        {
            var result = organBUS.OrganSearch(searchStr);
            return Ok(result);
        }
        [HttpGet("{organID}")]
        public IActionResult GetOrganByID(int organID)
        {
            var result = organBUS.GetOrganByID(organID);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetOrganByOrganTypeID(int organTypeID)
        {
            var result = organBUS.GetOrganByOrganTypeID(organTypeID);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetOrganByAddressID(int addressID)
        {
            var result = organBUS.GetOrganByAddressID(addressID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteOrgan(int OrganID)
        {
            var result = organBUS.DeleteOrgan(OrganID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateOrgan(Organ organ)
        {
            Organ organModify = new Organ();
            organModify.OrganID = organ.OrganID;
            organModify.OrganName = organ.OrganName;
            organModify.Status = organ.Status;
            organModify.Deleted = organ.Deleted;
            organModify.OrganTypeID = organ.OrganTypeID;
            organModify.AddressID = organ.AddressID;
            organModify.Note = organ.Note;
            DateTime currentDate = new DateTime();
            currentDate = DateTime.Now;
            organModify.UpdateTime = currentDate;
            var result = organBUS.UpdateOrgan(organModify);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult InsertOrgan(Organ organ)
        {
            Organ organModify = new Organ();
            organModify.OrganName = organ.OrganName;
            organModify.Status = organ.Status;
            organModify.Deleted = organ.Deleted;
            organModify.OrganTypeID = organ.OrganTypeID;
            organModify.AddressID = organ.AddressID;
            organModify.Note = organ.Note;
            organModify.Note = organ.Note;
            DateTime currentDate = new DateTime();
            currentDate = DateTime.Now;
            organModify.CreateTime = currentDate;
            var result = organBUS.InsertOrgan(organModify);
            return Ok(result);
        }
    }
}