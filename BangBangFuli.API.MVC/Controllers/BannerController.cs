using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVC.Models;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using BangBangFuli.H5.API.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace BangBangFuli.API.MVC.Controllers
{
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BannerController(IBannerService bannerService, IWebHostEnvironment hostingEnvironment)
        {
            _bannerService = bannerService;
            _hostingEnvironment = hostingEnvironment;
        }

        #region webapi


        /// <summary>
        /// 获取banner根据批次时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/v{version:apiVersion}/BasicData/Banner/{batchCode}")]
        public ResponseOutput GetBannerByBatchCode(string batchCode)
        {
            var photoUniqueNames = _bannerService.GetUniquePhotoNamesByBatchCode(batchCode);
            return new ResponseOutput(photoUniqueNames, HttpContext.TraceIdentifier);
        }


        #endregion

        /// <summary>
        /// 创建banner视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateSave(BannerViewModel model)
        {
            if (ModelState.IsValid)
            {

                List<string> uniqueFileNameList = null;

                if (model.Photos != null && model.Photos.Count > 0)
                {
                    uniqueFileNameList = ProcessUploadedFile(model);

                }
                foreach (var uniqueFileName in uniqueFileNameList)
                {
                    Banner banner = new Banner
                    {
                        BatchCode = model.BatchCode,
                        Photo = uniqueFileName
                    };
                    _bannerService.Save(banner);
                }

                return RedirectToAction("Detail",new { });
            }
            return View();
        }

        /// <summary>
        /// 将照片保存到指定的路径中，并返回唯一的文件名
        /// </summary>
        /// <returns></returns>
        private List<string> ProcessUploadedFile(BannerViewModel model)
        {
            var photoFileNameList = new List<string>();

            if (model.Photos.Count > 0)
            {
                foreach (var photo in model.Photos)
                {
                    string uniqueFileName = null;
                    //必须将图像上传到wwwroot中的images文件夹
                    //而要获取wwwroot文件夹的路径，我们需要注入 ASP.NET Core提供的HostingEnvironment服务
                    //通过HostingEnvironment服务去获取wwwroot文件夹的路径
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    //为了确保文件名是唯一的，我们在文件名后附加一个新的GUID值和一个下划线

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    //因为使用了非托管资源，所以需要手动进行释放
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        //使用IFormFile接口提供的CopyTo()方法将文件复制到wwwroot/images文件夹
                        photo.CopyTo(fileStream);
                    }

                    photoFileNameList.Add(uniqueFileName);
                }
            }
            return photoFileNameList;

        }


    }
}