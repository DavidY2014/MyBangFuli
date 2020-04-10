using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Filter;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using Microsoft.AspNetCore.Mvc;

namespace BangBangFuli.API.MVCDotnet2.Controllers.Version1
{
    [UserLoginFilter]
    public class SupplierController : BaseController
    {
        private IUserRoleJurisdictionService _userRoleJurisdictionService;
        private IModuleInfoService _moduleInfoService;

        public SupplierController(IUserRoleJurisdictionService userRoleJurisdictionService, IModuleInfoService moduleInfoService)
            : base(userRoleJurisdictionService, moduleInfoService)
        {
            _moduleInfoService = moduleInfoService;
            _userRoleJurisdictionService = userRoleJurisdictionService;
        }
        /// <summary>
        /// 供应商列表页
        /// </summary>
        /// <returns></returns>
        public IActionResult QuerySupplierList()
        {
            return View();
        }

        /// <summary>
        /// 新增供应商信息
        /// </summary>
        /// <returns></returns>
        public IActionResult AddNewSupply()
        {
            return View();
        }
    }
}