using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentManagement.Model.Entity.OrganType;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrganTypeController : ControllerBase
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
    }
}