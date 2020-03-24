using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Models;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using BangBangFuli.H5.API.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BangBangFuli.API.MVCDotnet2.Controllers.Version1
{
    public class ProductsController : BaseController
    {
        private IUserRoleJurisdictionService _userRoleJurisdictionService;
        private IProductInformationService _productInformationService;
        private IBatchInformationService _batchInformationService;
        public ProductsController(IUserRoleJurisdictionService userRoleJurisdictionService, IProductInformationService productInformationService,
            IBatchInformationService batchInformationService)
        {
            _userRoleJurisdictionService = userRoleJurisdictionService;
            _productInformationService = productInformationService;
            _batchInformationService = batchInformationService;
        }


        #region 商品管理

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        public IActionResult ProductList()
        {
            ViewBag.root = "account";
            return View();
        }
        [HttpGet]
        public IActionResult ProductListData(int page, int limit)
        {
            List<ProductInformationViewModel> productList = new List<ProductInformationViewModel>();
            var productinfos = _productInformationService.GetList( page, limit);
            if (productinfos.Item1 != null && productinfos.Item1.Count > 0)
            {
                foreach (var product in productinfos.Item1)
                {
                    BatchInformation batchInfo = _batchInformationService.GetBatchInfoById(product.BatchId);
                    ProductInformationViewModel vm = new ProductInformationViewModel();
                    //vm.ProductId = product.Id;
                    //vm.ProductCode = product.ProductCode;
                    //vm.ProductName = product.ProductName;
                    vm.StockStatusType = product.StockType;
                    vm.ProductStatusType = product.ProductStatus;
                    vm.ClassType = product.Type;
                    vm.BatchId = product.BatchId;
                    vm.BatchName = batchInfo != null ? batchInfo.Name : string.Empty;
                    productList.Add(vm);
                }
            }
            return Json(new { code = 0, msg = "", count = productinfos.Item2, data = productList.ToArray() });
        }
        ///// <summary>
        ///// 添加用户
        ///// </summary>
        ///// <returns></returns>
        //public IActionResult AddUser(int id = 0)
        //{
        //    var userRolelist = _userRoleService.Get();
        //    ViewBag.userRolelist = userRolelist;
        //    ViewBag.root = "account";
        //    UserInfo userInfo = new UserInfo();
        //    if (id > 0)
        //    {
        //        userInfo = _userService.GetByID(id);
        //    }
        //    ViewBag.userInfo = userInfo;
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult SaveUser()
        //{
        //    UserInfo user = new UserInfo();
        //    user.UserName = Request.Form["UserName"].TryToString();
        //    string Password = Request.Form["Password"].TryToString();
        //    user.CreateTime = DateTime.Now;
        //    user.Name = Request.Form["Name"].TryToString();
        //    user.TelPhone = Request.Form["TelPhone"].TryToString();
        //    user.RoleID = Request.Form["RoleID"].TryToInt();
        //    user.Id = Request.Form["ID"].TryToInt();
        //    if (user.Id > 0)
        //    {
        //        bool flag = _userService.UpdateUserInfo(user, Password);
        //        if (flag)
        //            return Json(new { code = 1, msg = "成功" });
        //    }
        //    else
        //    {
        //        int id = _userService.AddUser(user, Password);
        //        if (id > 0)
        //            return Json(new { code = 1, msg = "成功" });
        //    }

        //    return Json(new { code = 0, msg = "失败" });
        //}
        //[HttpGet]
        //public IActionResult DelUser(int id)
        //{
        //    var user = _userService.GetByID(id);
        //    if (user != null)
        //    {
        //        user.State = StateEnum.Valid;
        //        bool flag = _userService.UpdateUserInfo(user);
        //        if (flag)
        //            return Json(new { code = 1, msg = "OK" });
        //    }
        //    return Json(new { code = 0, msg = "删除失败" });
        //}

        //public IActionResult UserMessage()
        //{
        //    UserRole userRole = _userRoleService.Get(User.RoleID);
        //    ViewBag.userRole = userRole;
        //    return View(User);
        //}




        #endregion


    }
}