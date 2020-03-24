using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Models;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using BangBangFuli.H5.API.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangBangFuli.API.MVCDotnet2.Controllers
{
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IBannerDetailService _bannerDetailService;
        private readonly IBatchInformationService _batchInformationService;
        public BannerController(IBannerService bannerService, IHostingEnvironment hostingEnvironment,IBannerDetailService bannerDetailService, IBatchInformationService batchInformationService)
        {
            _bannerService = bannerService;
            _hostingEnvironment = hostingEnvironment;
            _bannerDetailService = bannerDetailService;
            _batchInformationService = batchInformationService;
        }

        public IActionResult Index()
        {
            List<BannerViewModel> bannerViewModels = new List<BannerViewModel>();
            List<Banner> banners = _bannerService.GetAll();
            foreach (var banner in banners)
            {
                bannerViewModels.Add(new BannerViewModel
                {
                    BannerId = banner.Id,
                    BatchId =banner.BatchId,
                    Name = banner.Name,
                    CreateTime = banner.CreateTime
                });
            }
            return View(bannerViewModels);
        }

        /// <summary>
        /// banner编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            PopulateBatchDropDownList();
            BannerViewModel model = new BannerViewModel();
            if (id != null)
            {
                Banner banner = _bannerService.GetBannerById((int)id);
                model = new BannerViewModel
                {
                    BannerId = banner.Id,
                    Name = banner.Name,
                    BatchId = banner.BatchId
                };
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult EditSave(BannerViewModel model)
        {

            List<string> uniqueFileNameList = null;

            if (model.Photos != null && model.Photos.Count > 0)
            {
                uniqueFileNameList = ProcessUploadedFile(model);
            }
            var details = new List<BannerDetail>();
            if (uniqueFileNameList != null && uniqueFileNameList.Count > 0)
            {
                foreach (var uniqueFileName in uniqueFileNameList)
                {
                    details.Add(new BannerDetail
                    {
                        PhotoPath = uniqueFileName
                    });
                }
            }
            Banner banner = new Banner
            {
                Id = model.BannerId,
                BatchId = model.BatchId,
                Name = model.Name,
                CreateTime = DateTime.Now,
                BannerDetails = details
            };
            _bannerService.UpdateBanner(banner);

            return RedirectToAction(nameof(Index));

        }

        //删除
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            _bannerService.RemoveBannerById((int)id);
            //图片删除由自动任务实现，不然会影响性能
            return RedirectToAction(nameof(Index));
        }


        public ActionResult Detail()
        {
            return View();
        }

        //详情界面，图片渲染
        public IActionResult Details(int id)
        {
            List<BannerDetailViewModel> vmdetails = new List<BannerDetailViewModel>();
            List<BannerDetail> details = _bannerDetailService.GetDetailsByBannerId(id);
            foreach (var detail in details)
            {
                vmdetails.Add(new BannerDetailViewModel()
                {
                    PhotoPath = detail.PhotoPath
                });
            }

            return View(vmdetails);
        }


        /// <summary>
        /// 创建banner新建视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            PopulateBatchDropDownList();
            return View();
        }


        [HttpPost]
        public IActionResult CreateSave(BannerViewModel model)
        {
            List<string> uniqueFileNameList = null;

            if (model.Photos != null && model.Photos.Count > 0)
            {
                uniqueFileNameList = ProcessUploadedFile(model);
            }
            var details = new List<BannerDetail>();
            if (uniqueFileNameList != null && uniqueFileNameList.Count > 0)
            {
                foreach (var uniqueFileName in uniqueFileNameList)
                {
                    details.Add(new BannerDetail
                    {
                        PhotoPath = uniqueFileName
                    });
                }
            }

            var bannerInfo = _batchInformationService.GetBatchInfoById(model.BatchId);

            Banner banner = new Banner
            {
                BatchId = model.BatchId,
                Name = bannerInfo.Name,
                CreateTime = DateTime.Now,
                BannerDetails = details
            };
            _bannerService.Save(banner);

            return RedirectToAction(nameof(Index));

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
                    string fileNameWithoutExtension =System.IO.Path.GetFileName(photo.FileName);

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileNameWithoutExtension;
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


        #region 批次号下拉列表
        private void PopulateBatchDropDownList(object selectedBatch = null)
        {
            var batchs = new List<object>();
            List<BatchInformation> batchInfos =  _batchInformationService.GetAll();
            foreach (var batch in batchInfos)
            {
                batchs.Add(new { id = batch.Id, name = batch.Name});
            }
            ViewBag.BatchIds = new SelectList(batchs, "id", "name", selectedBatch);
        }

        #endregion

    }
}