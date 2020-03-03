using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Model.Entity.Address;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressBUS addressBUS = AddressBUS.GetAddressBUSInstance;

        //[HttpGet]
        //[Route("{id}")]
        //public IActionResult GetAllTinh()
        //{
        //    return Ok(addressBUS.GetAllTinh());
        //}

        [HttpGet]
        public IActionResult GetDistrictByProvinceID([FromQuery] int provinceId)
        {
            IList<District> districts = new List<District>();
            districts = addressBUS.GetDistrictByProvinceID(provinceId).ItemList.Select(item =>
            {
                return new District()
                {
                    DistrictID = item.DistrictID,
                    DistrictName = item.DistrictName,
                    Level = item.Level,
                    ProvincialID = item.ProvincialID
                };
            }).Distinct().ToList();
            return Ok(districts);
        }

        /// <summary>
        /// lấy danh sách xã, phường, thị trấn của huyện
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetWardByDistrictID([FromQuery] int districtId)
        {
            IList<Wards> wards = new List<Wards>();
            wards = addressBUS.GetWardByDistrictID(districtId).ItemList
                .Select(item =>
                {
                    return new Wards()
                    {
                        DistrictID = item.DistrictID,
                        WardsID = item.WardsID,
                        Level = item.Level,
                        WardsName = item.WardsName
                    };
                }).Distinct().ToList();
            return Ok(wards);
        }

        /// <summary>
        /// lấy danh sách các tỉnh
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllProvince()
        {
            IList<Provincial> provincials = new List<Provincial>();
            provincials = addressBUS.GetAllTinh().ItemList.Select(item =>
            {
                return new Provincial()
                {
                    ProvincialID = item.ProvincialID,
                    ProvincialName = item.ProvincialName,
                    Level = item.Level
                };
            }).Distinct<Provincial>().ToList();
            return Ok(provincials);
        }

        /// <summary>
        /// lấy danh sách xã phường với id của tỉnh
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllWardsByProvinceId(int provinceId)
        {
            IList<Wards> lstWards = new List<Wards>();
            lstWards = addressBUS.GetAllWardsByProvinceId(provinceId).ItemList;
            return Ok(lstWards);
        }

    }
}