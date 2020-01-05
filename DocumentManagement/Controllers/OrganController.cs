using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Model.Entity.Organ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAllOrgan()
        {
            OrganBUS organBUS = new OrganBUS();
            var result = organBUS.GetAllOrgan();
            return Ok(result);
        }
        [HttpGet]
        public IActionResult Search(string searchStr)
        {
            OrganBUS organBUS = new OrganBUS();
            var result = organBUS.OrganSearch(searchStr);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetOrganByID(int OrganID)
        {
            OrganBUS organBUS = new OrganBUS();
            var result = organBUS.GetOrganByID(OrganID);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetOrganByOrganTypeID(int organTypeID)
        {
            OrganBUS organBUS = new OrganBUS();
            var result = organBUS.GetOrganByOrganTypeID(organTypeID);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetOrganByAddressID(int addressID)
        {
            OrganBUS organBUS = new OrganBUS();
            var result = organBUS.GetOrganByAddressID(addressID);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult DeleteOrgan(int OrganID)
        {
            OrganBUS organBUS = new OrganBUS();
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
            OrganBUS organBUS = new OrganBUS();
            var result = organBUS.UpdateOrgan(organModify);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult InsertOrgan(Organ organ)
        {
            OrganBUS organBUS = new OrganBUS();
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