using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Models;
using BangBangFuli.Common;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using BangBangFuli.H5.API.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace BangBangFuli.API.MVCDotnet2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login()
        {
            string username = Request.Form["username"].TryToString();
            string password = Request.Form["password"].TryToString();
            UserInfo user = _userService.UserLogin(username,password);
            if (user != null)
            {

                HttpContext.Session.SetString("user", JsonSerializerHelper.Serialize(user));
                return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "登录失败" });
        }

        public IActionResult LoginOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }



    }
}