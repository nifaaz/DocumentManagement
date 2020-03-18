using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentManagement.BUS;
using DocumentManagement.Common;
using DocumentManagement.Models.Entity.User;
using Common.Common;

namespace DocumentManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly UserBUS userBUS = UserBUS.GetUserBUSInstance;

        [HttpGet]
        public IActionResult UserGetSearchWithPaging ([FromQuery] BaseCondition<User> condition)
        {
            ReturnResult<User> result = userBUS.UserGetSearchWithPaging(condition);
            return Ok(result);
        }

        //// GET: User/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: User/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
           
            return Ok();
        }

        //// GET: User/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: User/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    return View();
        //}

        //// GET: User/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: User/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    return View();
        //}
    }
}