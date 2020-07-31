using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.FrameWork;
using DocumentManagement.Model.Entity;
using DocumentManagement.Model.Entity.GearBox;
using DocumentManagement.Model.Entity.TableOfContens;
using DocumentManagement.Models.DTO;
using DocumentManagement.Models.Entity.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    public class GearBoxController : BaseApiController
    {
        private static GearBoxBUS gearBoxBUS = GearBoxBUS.GetGearBoxBUSInstance;

        [HttpPost]
        public async Task<IActionResult> GetPagingWithSearchResults([FromBody]BaseCondition<GearBox> condition)
        {
            var result = await gearBoxBUS.GetPagingWithSearchResults(condition);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult SearchGearBox(string searchStr)
        {
            var result = gearBoxBUS.GearBoxSearch(searchStr);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGearBox()
        {
            var result = await gearBoxBUS.GetAllGearBox();
            return Ok(result);
        }
        [HttpGet("{gearBoxID}")]
        public async Task<IActionResult> GetGearBoxByID(int gearBoxID)
        {
            var result = await gearBoxBUS.GetGearBoxByID(gearBoxID);
            return Ok(result);
        }
        [HttpPost]

        public async Task<IActionResult> GetGearBoxByTabOfContID([FromBody]BaseCondition<GearBox> condition)
        {
            var result = await gearBoxBUS.GetGearBoxByTabOfContID(condition);
            return Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGearBoxByTabOfContID(string id)
        {
            var result = await gearBoxBUS.GetGearBoxByTabOfContID(id, 3);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGearBoxByTabOfContForEditID(string id)
        {
            var result = await gearBoxBUS.GetGearBoxByTabOfContID(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> GetProfileByGearBoxID([FromBody]BaseCondition<Profiles> condition)
        {
            var result = await gearBoxBUS.GetProfileByGearBoxID(condition);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateGearBox([FromBody]GearBox gearBox)
        {
            gearBox.isDeleted = 0;
            DateTime currentDate = DateTime.Now;
            gearBox.CreateTime = currentDate;
            var result = gearBoxBUS.CreateGearBox(gearBox);
            return Ok(result);
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
        public async Task<IActionResult> GetFontsByOrganIDSelect2([FromQuery] int organID)
        {
            var fonts = await gearBoxBUS.GetFontsByOrganIDSelect2(organID);
            return Ok(fonts.ItemList);
        }

        [HttpGet]
        public async Task<IActionResult> GetTableOfContentsByFontIDSelect2([FromQuery] int fontID)
        {
            var tableOfContents =await gearBoxBUS.GetTableOfContentsByFontIDSelect2(fontID);
            return Ok(tableOfContents.ItemList);
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
                    TabOfContNumber = x.TabOfContNumber,
                };
            }).Distinct().ToList();
            return Ok(tableOfContents);
        }
    }
}