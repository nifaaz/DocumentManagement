using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Model.Entity.GearBox;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GearBoxController : ControllerBase
    {
        [HttpGet]
        public IActionResult SearchGearBox(string searchStr)
        {
            GearBoxBUS gearBoxBUS = new GearBoxBUS();
            var result = gearBoxBUS.GearBoxSearch(searchStr);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetALlGearBox()
        {
            GearBoxBUS gearBoxBUS = new GearBoxBUS();
            var result = gearBoxBUS.GetAllGearBox();
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetGearBoxByID(int gearBoxID)
        {
            GearBoxBUS gearBoxBUS = new GearBoxBUS();
            var result = gearBoxBUS.GetGearBoxByID(gearBoxID);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetGearBoxByTabOfContID(int tabOfContID)
        {
            GearBoxBUS gearBoxBUS = new GearBoxBUS();
            var result = gearBoxBUS.GetGearBoxByTabOfContID(tabOfContID);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateGearBox(GearBox gearBox)
        {
            GearBoxBUS gearBoxBUS = new GearBoxBUS();
            var result = gearBoxBUS.CreateGearBox(gearBox);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateGearBox(GearBox gearBox)
        {
            GearBoxBUS gearBoxBUS = new GearBoxBUS();
            var result = gearBoxBUS.UpdateGearBox(gearBox);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteGearBox(int gearBoxId)
        {
            GearBoxBUS gearBoxBUS = new GearBoxBUS();
            var result = gearBoxBUS.DeleteGearBox(gearBoxId);
            return Ok(result);
        }
    }
}