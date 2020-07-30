using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentManagement.Model.Entity.OrganType;
using DocumentManagement.Common;
using DocumentManagement.FrameWork;

namespace DocumentManagement.Controllers
{
    public class OrganTypeController : BaseApiController
    {

        private LoaiCoQuanBUS loaiCoQuanBUS = LoaiCoQuanBUS.GetLoaiCoQuanBUSInstance();

        [HttpGet]
        public IActionResult GetAllOrganType()
        {
            List<OrganType> lstOrganType = new List<OrganType>();
            lstOrganType = loaiCoQuanBUS.GetAllOrganType().ItemList.Select(item => {
                return new OrganType() {
                    OrganTypeID = item.OrganTypeID,
                    OrganTypeName = item.OrganTypeName
                }; 
            }).Distinct().ToList();
            return Ok(lstOrganType);
        }

        [HttpPost]
        public IActionResult OrganTypeGetSearchWithPaging([FromBody] BaseCondition<OrganType> condition)
        {
            ReturnResult<OrganType> result = loaiCoQuanBUS.OrganTypeGetSearchWithPaging(condition);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateOrganType(OrganType OrganType)
        {
            var result = loaiCoQuanBUS.CreateOrganType(OrganType);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateOrganType(OrganType OrganType)
        {
            var result = loaiCoQuanBUS.EditOrganType(OrganType);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteOrganType([FromQuery] int id)
        {
            return Ok(loaiCoQuanBUS.DeleteOrganType(id));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOrganTypeByID(int id)
        {
            return Ok(loaiCoQuanBUS.GetOrganTypeByID(id));
        }

    }
}