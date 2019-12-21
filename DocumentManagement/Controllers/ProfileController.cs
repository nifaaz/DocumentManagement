using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Model.Entity.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetALlProfile()
        {
            ProfileBUS profileBUS = new ProfileBUS();
            var result = profileBUS.GetAllProfile();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateProfile(Profile profile)
        {
            ProfileBUS profileBUS = new ProfileBUS();
            var result = profileBUS.CreateProfile(profile);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpdateProfile(Profile profile)
        {
            ProfileBUS profileBUS = new ProfileBUS();
            var result = profileBUS.UpdateProfile(profile);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteProfile(int profileId)
        {
            ProfileBUS profileBUS = new ProfileBUS();
            var result = profileBUS.DeleteProfile(profileId);
            return Ok(result);
        }
    }
}