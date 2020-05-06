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
        private readonly IRabbitMqProducer _mqProducer;

        public EnterCustomController(IUserRoleJurisdictionService userRoleJurisdictionService,
            IModuleInfoService moduleInfoService,
            IRabbitMqProducer mqProducer) :base(userRoleJurisdictionService, moduleInfoService)
        {
            _userRoleJurisdictionService = userRoleJurisdictionService;
            _moduleInfoService = moduleInfoService;
            _mqProducer = mqProducer;
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

        [HttpPost]
        public IActionResult SendMsg()
        {
            var msg = Request.Form["Msg"].TryToString();
            _mqProducer.SendMessage(msg);
            //return Json(new { code = 0, msg = $"发送消息到mq为:{msg}" });
            return RedirectToAction("ConsoleIndex");
        }


        #endregion



 



       
    }
}