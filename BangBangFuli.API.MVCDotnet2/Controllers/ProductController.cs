using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Models;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using BangBangFuli.H5.API.Core;
using BangBangFuli.H5.API.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UEditorNetCore;

namespace BangBangFuli.API.MVCDotnet2.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductInformationService _productInformationService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProductDetailService _productDetailService;
        private readonly IBatchInformationService _batchInformationService;

        public ProductController(IProductInformationService productInformationService ,IHostingEnvironment hostingEnvironment
            ,IProductDetailService productDetailService, IBatchInformationService batchInformationService)
        {
            _productInformationService = productInformationService;
            _hostingEnvironment = hostingEnvironment;
            _productDetailService = productDetailService;
            _batchInformationService = batchInformationService;
        }
        public IActionResult Index()
        {
            var productViewModelList = new List<ProductInformationViewModel>();
            var products = _productInformationService.GetAll();
            foreach (var product in products)
            {
                BatchInformation batchInfo = _batchInformationService.GetBatchInfoById(product.BatchId);
                productViewModelList.Add(new ProductInformationViewModel
                {
                    ProductId = product.Id,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    StockStatusName = GetStockStatusDisplayName((int)product.StockType),
                    ProductStatusName = GetProductStatusDisplayName((int)product.ProductStatus),
                    ClassTypeName = GetClassTypeDisplayName((int)product.Type),
                    StockStatusType = product.StockType,
                    ProductStatusType = product.ProductStatus,
                    ClassType = product.Type,
                    BatchId = product.BatchId,
                    BatchName = batchInfo.Name
                }) ;
            }
            return View(productViewModelList);
        }


        /// <summary>
        /// 富文本界面
        /// </summary>
        /// <returns></returns>
        public IActionResult NewEdit(int productId)
        {
            ViewBag.productId = productId ;
            return View();
        }

        public IActionResult Details(int id)
        {
            List<ProductDetailViewModel> detailViewModels = new List<ProductDetailViewModel>();
            List<ProductDetail> details =  _productDetailService.GetDetailsByProductId(id);
            foreach (var item in details)
            {
                detailViewModels.Add(new ProductDetailViewModel
                {
                    PhotoPath = item.PhotoPath
                });
            }
            return View(detailViewModels);
        }

        /// <summary>
        /// 新增编辑界面
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ProductInformationViewModel model = new ProductInformationViewModel();
            PopulateClassDropDownList();
            PopulateProductStatusDropDownList();
            PopulateStockStatusDropDownList();
            PopulateBatchDropDownList();
            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            ProductInformationViewModel model = new ProductInformationViewModel();
            //编辑界面
            if (id != null)
            {
                ProductInformation product = _productInformationService.GetProductById((int)id);
                model = new ProductInformationViewModel
                {
                    ProductId = product.Id,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    ProductStatusType = product.ProductStatus,
                    StockStatusType=product.StockType,
                    ClassType = product.Type,
                    Description = product.Description,
                    BatchId = product.BatchId
                };
            }
            PopulateClassDropDownList();
            PopulateProductStatusDropDownList();
            PopulateStockStatusDropDownList();
            PopulateBatchDropDownList();
            return View(model);
        }



        //删除
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            _productInformationService.RemoveProductById((int)id);
            //图片删除由自动任务实现，不然会影响性能
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 商品描述信息
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveProductDescription(string description,int productId)
        {
            if (productId>0)
            {
                ProductInformation product = _productInformationService.GetProductById(productId);
                product.Description = description;
                _productInformationService.UpdateProduct(product);
            }
            return View();
        }


        [HttpPost]
        public IActionResult CreateSave(ProductInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> uniqueFileNameList = null;

                if (model.Photos != null && model.Photos.Count > 0)
                {
                    uniqueFileNameList = ProcessUploadedFile(model);

                }
                var details = new List<ProductDetail>();
                if (uniqueFileNameList!=null)
                {
                    foreach (var uniqueFileName in uniqueFileNameList)
                    {
                        details.Add(new ProductDetail { PhotoPath = uniqueFileName });
                    }
                }
                ProductInformation product = new ProductInformation
                {
                    ProductCode = model.ProductCode,
                    ProductName = model.ProductName,
                    ProductStatus =  model.ProductStatusType,
                    StockType = model.StockStatusType,
                    Type = model.ClassType,
                    BatchId = model.BatchId,
                    Details = details
                };

                _productInformationService.Save(product);
                return RedirectToAction(nameof(NewEdit),new { productId=product.Id});
            }
            return View(model);
        }



        [HttpPost]
        public IActionResult EditSave(ProductInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> uniqueFileNameList = null;

                if (model.Photos != null && model.Photos.Count > 0)
                {
                    uniqueFileNameList = ProcessUploadedFile(model);

                }
                var details = new List<ProductDetail>();
                if (uniqueFileNameList != null)
                {
                    foreach (var uniqueFileName in uniqueFileNameList)
                    {
                        details.Add(new ProductDetail { PhotoPath = uniqueFileName });
                    }
                }
                ProductInformation product = new ProductInformation
                {
                    Id= model.ProductId,
                    ProductCode = model.ProductCode,
                    ProductName = model.ProductName,
                    StockType = model.StockStatusType,
                    ProductStatus = model.ProductStatusType,
                    Type = model.ClassType,
                    BatchId = model.BatchId,
                    Details = details
                };

                _productInformationService.UpdateProduct(product);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        #region 分类枚举
        private void PopulateClassDropDownList(object selectedClass = null)
        {
            var productTypes = new List<object>();
            productTypes.Add(new { id = 0, name = "悦享生活" });
            productTypes.Add(new { id = 1, name = "居家好物" });
            productTypes.Add(new { id = 2, name = "品质生活" });
            productTypes.Add(new { id = 3, name = "厨房甄选" });
            ViewBag.Catelogs = new SelectList(productTypes, "id", "name", selectedClass);
        }


        private void PopulateProductStatusDropDownList(object selectedProductStatus= null)
        {
            var productStatusTypes = new List<object>();
            productStatusTypes.Add(new { id = 0, name = "下架" });
            productStatusTypes.Add(new { id = 1, name = "上架" });
            ViewBag.ProductStatusTypes = new SelectList(productStatusTypes, "id", "name", selectedProductStatus);
        }

        private void PopulateStockStatusDropDownList(object selectedStockStatus = null)
        {
            var stockStatusTypes = new List<object>();
            stockStatusTypes.Add(new { id = 0, name = "无货" });
            stockStatusTypes.Add(new { id = 1, name = "有货" });
            ViewBag.StockStatusTypes = new SelectList(stockStatusTypes, "id", "name", selectedStockStatus);
        }

        private void PopulateBatchDropDownList(object selectedBatch = null)
        {
            var batchs = new List<object>();
            List<BatchInformation> batchInfos = _batchInformationService.GetAll();
            foreach (var batch in batchInfos)
            {
                batchs.Add(new { id = batch.Id, name = batch.Name });
            }
            ViewBag.BatchIds = new SelectList(batchs, "id", "name", selectedBatch);
        }


        public string GetClassTypeDisplayName(int index)
        {
            var ret = string.Empty;
            switch (index)
            {
                case 0:
                    ret = "悦享生活";
                    break;
                case 1:
                    ret = "居家好物";
                    break;
                case 2:
                    ret = "品质生活";
                    break;
                case 3:
                    ret = "厨房甄选";
                    break;
            }
            return ret;
        }

        public string GetProductStatusDisplayName(int index)
        {
            var ret = string.Empty;
            switch (index)
            {
                case 0:
                    ret = "下架";
                    break;
                case 1:
                    ret = "上架";
                    break;
            }
            return ret;
        }

        public string GetStockStatusDisplayName(int index)
        {
            var ret = string.Empty;
            switch (index)
            {
                case 0:
                    ret = "无货";
                    break;
                case 1:
                    ret = "有货";
                    break;
            }
            return ret;
        }

        #endregion

        #region 商品状态枚举

        public ClassTypeEnum GetClassTypeMap(int index)
        {
            ClassTypeEnum ret = ClassTypeEnum.unknown;
            switch (index)
            {
                case 0:
                    ret = ClassTypeEnum.yuexiangmeiwei;
                    break;
                case 1:
                    ret = ClassTypeEnum.jujiahaowu;
                    break;
                case 2:
                    ret = ClassTypeEnum.pingzhishenghuo;
                    break;
                case 3:
                    ret = ClassTypeEnum.chufangzhengxuan;
                    break;
            }
            return ret;


        }

        public ProductStatusTypeEnum GetProductStatusMap(int index)
        {
            ProductStatusTypeEnum ret = ProductStatusTypeEnum.Unknown;
            switch (index)
            {
                case 0:
                    ret = ProductStatusTypeEnum.Down;
                    break;
                case 1:
                    ret = ProductStatusTypeEnum.On;
                    break;
                default:
                    ret = ProductStatusTypeEnum.Unknown;
                    break;
            }
            return ret;

        }

        public StockStatusTypeEnum GetStockStatusMap(int index)
        {
            StockStatusTypeEnum ret = StockStatusTypeEnum.Unknown;
            switch (index)
            {
                case 0:
                    ret = StockStatusTypeEnum.No;
                    break;
                case 1:
                    ret = StockStatusTypeEnum.Yes;
                    break;
                default:
                    ret = StockStatusTypeEnum.Unknown;
                    break;
            }
            return ret;
        }


        #endregion





        #region 上传图片文件到wwwroot目录

        /// <summary>
        /// 将照片保存到指定的路径中，并返回唯一的文件名
        /// </summary>
        /// <returns></returns>
        private List<string> ProcessUploadedFile(ProductInformationViewModel model)
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

        #endregion

    }
}