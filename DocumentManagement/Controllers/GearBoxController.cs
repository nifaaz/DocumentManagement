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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GearBoxController : ControllerBase
    {
        private static GearBoxBUS gearBoxBUS = GearBoxBUS.GetGearBoxBUSInstance;
        [HttpGet]
        public IActionResult SearchGearBox(string searchStr)
        {
            var result = gearBoxBUS.GearBoxSearch(searchStr);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetAllGearBox()
        {
            var result = gearBoxBUS.GetAllGearBox();
            return Ok(result);
        }
        [HttpGet("{gearBoxID}")]
        public IActionResult GetGearBoxByID(int gearBoxID)
        {
            var result = gearBoxBUS.GetGearBoxByID(gearBoxID);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetGearBoxByTabOfContID(int tabOfContID)
        {
            var result = gearBoxBUS.GetGearBoxByTabOfContID(tabOfContID);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateGearBox(GearBox gearBox)
        {
            var result = gearBoxBUS.CreateGearBox(gearBox);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateGearBox(GearBox gearBox)
        {
            var result = gearBoxBUS.UpdateGearBox(gearBox);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteGearBox(int gearBoxId)
        {
            var result = gearBoxBUS.DeleteGearBox(gearBoxId);
            return Ok(result);
        }
    }
}