using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Filter;
using BangBangFuli.API.MVCDotnet2.Models;
using BangBangFuli.Common;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using BangBangFuli.H5.API.Core;
using BangBangFuli.H5.API.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangBangFuli.API.MVCDotnet2.Controllers
{
    [UserLoginFilter]
    public class EnterCustomController : BaseController
    {
        private IUserRoleJurisdictionService _userRoleJurisdictionService;
        private IModuleInfoService _moduleInfoService;

        public EnterCustomController(IUserRoleJurisdictionService userRoleJurisdictionService,
            IModuleInfoService moduleInfoService):base(userRoleJurisdictionService, moduleInfoService)
        {
            _userRoleJurisdictionService = userRoleJurisdictionService;
            _moduleInfoService = moduleInfoService;

        }

        #region 工作台 首页

        /// <summary>
        /// 首页，工作台
        /// </summary>
        /// <returns></returns>
        public IActionResult ConsoleIndex()
        {
            return View();
        }

        #endregion



 



       
    }
}