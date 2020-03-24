using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Models;
using BangBangFuli.Common;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.Entities.Enumes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BangBangFuli.API.MVCDotnet2.Controllers
{
    public class UsersController : BaseController
    {
        private IUserRoleJurisdictionService _userRoleJurisdictionService;
        private IModuleInfoService _moduleInfoService;
        private readonly IUserRoleService _userRoleService;
        private readonly IUserService _userService;

        public UsersController(IUserRoleJurisdictionService userRoleJurisdictionService, IModuleInfoService moduleInfoService,IUserRoleService userRoleService,
            IUserService userService):base(userRoleJurisdictionService,moduleInfoService)
        {
            _userRoleJurisdictionService = userRoleJurisdictionService;
            _moduleInfoService = moduleInfoService;
            _userRoleService = userRoleService;
            _userService = userService;
        }

        #region 模块管理

        public IActionResult ModuleList()
        {
            var list = _moduleInfoService.GetList();
            ViewBag.root = "account";
            return View(list);
        }

        [HttpPost]
        public IActionResult AddModule()
        {
            string name = Request.Form["name"].TryToString();
            ModuleInfo moduleInfo = new ModuleInfo();
            moduleInfo.Name = name;
            int id = _moduleInfoService.AddModuleInfo(moduleInfo);
            if (id > 0)
                return Json(new { code = 1, msg = "OK" });
            return Json(new { code = 0, msg = "保存失败" });
        }
        [HttpGet]
        public IActionResult DelModel(int id)
        {
            var info = _moduleInfoService.Get(id);
            if (info != null)
            {
                _moduleInfoService.DelModel(info);
                return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "删除失败" });
        }


        #endregion

        #region 角色管理

        /// <summary>
        /// 角色管理
        /// </summary>
        /// <returns></returns>
        public IActionResult RoleList()
        {
            var list = _userRoleService.Get();
            ViewBag.root = "account";
            return View(list);
        }

        [HttpPost]
        public IActionResult AddRole()
        {
            string name = Request.Form["name"].TryToString();
            UserRole userRole = new UserRole();
            userRole.RoleName = name;
            userRole.State = StateEnum.Invalid;
            int id = _userRoleService.AddRole(userRole);
            if (id > 0)
                return Json(new { code = 1, msg = "OK" });
            return Json(new { code = 0, msg = "失败" });
        }
        [HttpGet]
        public IActionResult DelRole(int id)
        {
            var info = _userRoleService.Get(id);
            if (info != null)
            {
                info.State = StateEnum.Valid;
                bool falg = _userRoleService.UpdateRole(info);
                if (falg)
                    return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "删除失败" });
        }

        /// <summary>
        /// 角色权限
        /// </summary>
        /// <returns></returns>
        public IActionResult AddRoleJuri(int roleid)
        {
            var list = _userRoleJurisdictionService.GetList(roleid);
            List<ModuleInfo> mlist = _moduleInfoService.GetList();
            ViewBag.mlist = mlist;

            UserRole userRole = _userRoleService.Get(roleid);
            ViewBag.userRole = userRole;
            ViewBag.roleid = roleid;
            ViewBag.root = "account";
            return View(list);
        }
        [HttpPost]
        public IActionResult SaveRoleJuri([FromForm]UserRoleJurisdiction userRoleJurisdiction)
        {
            if (userRoleJurisdiction.Id <= 0)
            {
                int id = _userRoleJurisdictionService.AddUserRoleJurisdiction(userRoleJurisdiction);
                if (id > 0)
                    return Json(new { code = 1, msg = "保存成功" });
                else
                    Json(new { code = 0, msg = "保存失败" });
            }
            else
            {
                bool flag = _userRoleJurisdictionService.UpdateUserRoleJurisdiction(userRoleJurisdiction);
                if (flag)
                    return Json(new { code = 1, msg = "保存成功" });
                else
                    return Json(new { code = 0, msg = "保存失败" });
            }
            return Json(new { code = 0, msg = "保存失败" });
        }


        #endregion


        #region 用户管理

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        public IActionResult UserList()
        {
            ViewBag.root = "account";
            return View();
        }
        [HttpGet]
        public IActionResult UserListData(int page, int limit)
        {
            List<UserViewModel> userlist = new List<UserViewModel>();
            var users = _userService.GetList("", page, limit);
            if (users.Item1 != null && users.Item1.Count > 0)
            {
                foreach (var user in users.Item1)
                {
                    UserViewModel vm = new UserViewModel();
                    vm.CreateTime = user.CreateTime.ToString("yyyy-MM-dd");
                    vm.ID = user.Id;
                    vm.Name = user.Name;
                    if (user.RoleID > 0)
                    {
                        var info = _userRoleService.Get(user.RoleID);
                        if (info != null)
                            vm.RoleName = info.RoleName;
                    }
                    vm.TelPhone = user.TelPhone;
                    vm.UserName = user.UserName;
                    userlist.Add(vm);
                }
            }
            return Json(new { code = 0, msg = "", count = users.Item2, data = userlist.ToArray() });
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        public IActionResult AddUser(int id = 0)
        {
            var userRolelist = _userRoleService.Get();
            ViewBag.userRolelist = userRolelist;
            ViewBag.root = "account";
            UserInfo userInfo = new UserInfo();
            if (id > 0)
            {
                userInfo = _userService.GetByID(id);
            }
            ViewBag.userInfo = userInfo;
            return View();
        }

        [HttpPost]
        public IActionResult SaveUser()
        {
            UserInfo user = new UserInfo();
            user.UserName = Request.Form["UserName"].TryToString();
            string Password = Request.Form["Password"].TryToString();
            user.CreateTime = DateTime.Now;
            user.Name = Request.Form["Name"].TryToString();
            user.TelPhone = Request.Form["TelPhone"].TryToString();
            user.RoleID = Request.Form["RoleID"].TryToInt();
            user.Id = Request.Form["ID"].TryToInt();
            if (user.Id > 0)
            {
                bool flag = _userService.UpdateUserInfo(user, Password);
                if (flag)
                    return Json(new { code = 1, msg = "成功" });
            }
            else
            {
                int id = _userService.AddUser(user, Password);
                if (id > 0)
                    return Json(new { code = 1, msg = "成功" });
            }

            return Json(new { code = 0, msg = "失败" });
        }
        [HttpGet]
        public IActionResult DelUser(int id)
        {
            var user = _userService.GetByID(id);
            if (user != null)
            {
                user.State = StateEnum.Valid;
                bool flag = _userService.UpdateUserInfo(user);
                if (flag)
                    return Json(new { code = 1, msg = "OK" });
            }
            return Json(new { code = 0, msg = "删除失败" });
        }

        public IActionResult UserMessage()
        {
            UserRole userRole = _userRoleService.Get(User.RoleID);
            ViewBag.userRole = userRole;
            return View(User);
        }


        #endregion


    }
}