using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Model.Entity;
using DocumentManagement.Model.Entity.GearBox;
using DocumentManagement.Model.Entity.TableOfContens;
using DocumentManagement.Models.Entity.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GearBoxController : ControllerBase
    {
        private static GearBoxBUS gearBoxBUS = GearBoxBUS.GetGearBoxBUSInstance;

        [HttpPost]
        public IActionResult GetPagingWithSearchResults([FromBody]BaseCondition<GearBox> condition)
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
        [HttpPost]
        public IActionResult GetGearBoxByTabOfContID([FromBody]BaseCondition<GearBox> condition)
        {
            var result = gearBoxBUS.GetGearBoxByTabOfContID(condition);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult GetProfileByGearBoxID([FromBody]BaseCondition<Profiles> condition)
        {
            var result = gearBoxBUS.GetProfileByGearBoxID(condition);
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

        [HttpGet]
        public IActionResult GetFontsByOrganID([FromQuery] int organID)
        {
            IList<Font> fonts = new List<Font>();
            fonts = gearBoxBUS.GetFontsByOrganID(organID).ItemList.Select(item =>
            {
                return new Font()
                {
                    FontID = item.FontID,
                    FontName = item.FontName,
                };
            }).Distinct().ToList();
            return Ok(fonts);
        }

        [HttpGet]
        public IActionResult GetTableOfContentsByFontID([FromQuery] int fontID)
        {
            IList<TableOfContents> tableOfContents = new List<TableOfContents>();
            tableOfContents = gearBoxBUS.GetTableOfContentsByFontID(fontID).ItemList.Select(x =>
            {
                return new TableOfContents()
                {
                    TabOfContID = x.TabOfContID,
                    TabOfContName = x.TabOfContName,
                };
            }).Distinct().ToList();
            return Ok(tableOfContents);
        }
    }
}