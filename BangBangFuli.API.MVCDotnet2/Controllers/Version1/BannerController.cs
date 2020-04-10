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
using BangBangFuli.H5.API.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BangBangFuli.API.MVCDotnet2.Controllers.Version1
{
    [UserLoginFilter]
    public class BannerController : BaseController
    {
        private IUserRoleJurisdictionService _userRoleJurisdictionService;
        private IModuleInfoService _moduleInfoService;

        private readonly IBatchInformationService _batchInformationService;
        private readonly IBannerService _bannerService;
        private readonly IBannerDetailService _bannerDetailService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BannerController(IUserRoleJurisdictionService userRoleJurisdictionService, IModuleInfoService moduleInfoService,IBannerService bannerService, 
            IBannerDetailService bannerDetailService,
            IBatchInformationService batchInformationService,
            IHostingEnvironment hostingEnvironment):base(userRoleJurisdictionService, moduleInfoService)
        {
            _moduleInfoService = moduleInfoService;
            _userRoleJurisdictionService = userRoleJurisdictionService;
            _hostingEnvironment = hostingEnvironment;
            _batchInformationService = batchInformationService;
            _bannerService = bannerService;
            _bannerDetailService = bannerDetailService;
        }

        /// <summary>
        /// banner 列表页面
        /// </summary>
        /// <returns></returns>
        public IActionResult QueryBannerList()
        {
            List<BannerViewModel> bannerViewModels = new List<BannerViewModel>();
            List<Banner> banners = _bannerService.GetAll();
            foreach (var banner in banners)
            {
                bannerViewModels.Add(new BannerViewModel
                {
                    BannerId = banner.Id,
                    BatchId = banner.BatchId,
                    Name = banner.Name,
                    CreateTime = banner.CreateTime
                });
            }
            return View(bannerViewModels);
        }

        /// <summary>
        /// 新建banner视图
        /// </summary>
        /// <returns></returns>
        public IActionResult AddNewBanner(int id)
        {
            Banner bannerInfo = new Banner();
            if (id > 0)
            {
                bannerInfo = _bannerService.GetBannerById(id);
            }
            List<BatchInformation> batchInfos = _batchInformationService.GetAll();
            ViewBag.BatchInfos = batchInfos;
            //ViewBag.SelectedBatchInfo = _batchInformationService.GetBatchInfoById(bannerInfo.BatchId);
            return View(bannerInfo);
        }

        [HttpPost]
        public IActionResult SaveBanner()
        {
            int id = Request.Form["ID"].TryToInt(0);
            if (id > 0)
            {
                var info = _bannerService.GetBannerById(id);
                info.Name = Request.Form["Name"].TryToString();
                info.BatchId = Request.Form["BatchId"].TryToInt(0);
                _bannerService.UpdateBanner(info);
                return Json(new { code = 1, msg = "OK", id = info.Id });
            }
            else
            {
                Banner bannerInfo = new Banner();
                bannerInfo.Name = Request.Form["Name"].TryToString();
                bannerInfo.BatchId = Request.Form["BatchId"].TryToInt(0);
                bannerInfo.CreateTime = DateTime.Now;
                id = _bannerService.AddBanner(bannerInfo);
                if (id > 0)
                {
                    return Json(new { code = 1, msg = "OK", id = id });
                }
                else
                {
                    return Json(new { code = 0, msg = "保存失败" });
                }
            }
        }

        /// <summary>
        /// banner 图片上传页面
        /// </summary>
        /// <param name="bannerId"></param>
        /// <returns></returns>
        public IActionResult AddBannerPhotos(int BannerId)
        {
            var bannerInfo = _bannerService.GetBannerById(BannerId);
            return View(bannerInfo);
        }


        [HttpGet]
        public IActionResult DelBanner(int id)
        {
            try
            {
                _bannerService.RemoveBannerById(id);
                return Json(new { code = 1, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "OK" });
            }
        }

        /// <summary>
        /// 上传banner图片
        /// </summary>
        /// <param name="BannerId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadBannerAttachment(int BannerId)
        {
            #region 文件上传
            var imgFile = Request.Form.Files[0];
            if (imgFile != null && !string.IsNullOrEmpty(imgFile.FileName))
            {
                string uniqueFileName = null;
                var filename = ContentDispositionHeaderValue
                     .Parse(imgFile.ContentDisposition)
                     .FileName
                     .Trim('"');
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "banners");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + filename;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    imgFile.CopyTo(fs);
                    fs.Flush();
                }

                #region banner 信息

                var bannerInfo = _bannerService.GetBannerById(BannerId);
                var details = new List<BannerDetail>();
                details.Add(new BannerDetail()
                {
                    BannerId = bannerInfo.Id,
                    PhotoPath = Path.Combine("banners", uniqueFileName)
                });
                bannerInfo.BannerDetails = details;
                _bannerService.UpdateBanner(bannerInfo);

                #endregion


                return Json(new { code = 0, msg = "上传成功", data = new { src = $"/images/{filePath}", title = "图片标题" } });
            }
            return Json(new { code = 1, msg = "上传失败", });
            #endregion
        }
    }
}