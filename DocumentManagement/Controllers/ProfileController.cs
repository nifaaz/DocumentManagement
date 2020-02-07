using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;

using DocumentManagement.Models.Entity.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private static ProfileBUS profileBUS = ProfileBUS.GetProfileBUSInstance;
        [HttpGet]
        public IActionResult GetAllProfile()
        {
            var result = profileBUS.GetAllProfile();
            return Ok(result);
        }
        [HttpGet("{profileID}")]
        public IActionResult GetProfileByID(int profileID)
        {
            var result = profileBUS.GetProfileByID(profileID);
            return Ok(result);
        }
        [HttpGet("{gearboxID}")]
        public IActionResult GetProfileByGeaBoxID(int gearboxID)
        {
            var result = profileBUS.GetProfileByGearBoxID(gearboxID);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateProfile(Profile profile)
        {
            var result = profileBUS.CreateProfile(profile);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateProfile(Profile profile)
        {
            var result = profileBUS.UpdateProfile(profile);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteProfile(int profileId)
        {
            var result = profileBUS.DeleteProfile(profileId);
            return Ok(result);
        }
    }
}