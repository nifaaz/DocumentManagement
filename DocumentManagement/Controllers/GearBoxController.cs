using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
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
        public IActionResult GetPagingWithSearchResults([FromQuery]BaseCondition<GearBox> condition)
        {
            var result = gearBoxBUS.GetPagingWithSearchResults(condition);
            return Ok(result);
        }

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
        public IActionResult CreateGearBox([FromBody]GearBox gearBox)
        {
            try
            {
                gearBox.isDeleted = 0;
                DateTime currentDate = DateTime.Now;
                gearBox.CreateTime = currentDate;
                var result = gearBoxBUS.CreateGearBox(gearBox);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult UpdateGearBox([FromBody]GearBox gearBox)
        {
            DateTime currentDate = DateTime.Now;
            gearBox.UpdateTime = currentDate;
            try
            {
                var result = gearBoxBUS.UpdateGearBox(gearBox);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult DeleteGearBox([FromQuery]int id)
        {
            try
            {
                var result = gearBoxBUS.DeleteGearBox(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}