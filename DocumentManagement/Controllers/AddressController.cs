using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressBUS addressBUS = AddressBUS.GetAddressBUSInstance;
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAllTinh()
        {
            return Ok(addressBUS.GetAllTinh());
        }
        public IActionResult GetHuyenByTinhID(int tinhID)
        {
            return Ok(addressBUS.GetHuyenByTinhID(tinhID));
        }
        public IActionResult GetXaPhuongByHuyenID(int huyenID)
        {
            return Ok(addressBUS.GetXaPhuongByHuyenID(huyenID));
        }

    }
}