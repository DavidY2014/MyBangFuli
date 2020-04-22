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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BangBangFuli.API.MVCDotnet2.Controllers.Version1
{
    [UserLoginFilter]
    public class ProductsController : BaseController
    {
        private IUserRoleJurisdictionService _userRoleJurisdictionService;
        private IModuleInfoService _moduleInfoService;
        private readonly IHostingEnvironment _hostingEnvironment;

        private IProductInformationService _productInformationService;
        private IBatchInformationService _batchInformationService;
        public ProductsController(IUserRoleJurisdictionService userRoleJurisdictionService, IModuleInfoService moduleInfoService,
            IProductInformationService productInformationService,
            IBatchInformationService batchInformationService,
            IHostingEnvironment hostingEnvironment)
             : base(userRoleJurisdictionService, moduleInfoService)
        {
            _userRoleJurisdictionService = userRoleJurisdictionService;
            _moduleInfoService = moduleInfoService;
            _hostingEnvironment = hostingEnvironment;

            _productInformationService = productInformationService;
            _batchInformationService = batchInformationService;
        }

        public IActionResult ProductList()
        {
            return View();
        }

        /// <summary>
        /// 商品页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ProductListData(int page, int limit)
        {
            List<ProductInformationViewModel> productList = new List<ProductInformationViewModel>();
            var productinfos = _productInformationService.GetList(page, limit);
            if (productinfos.Item1 != null && productinfos.Item1.Count > 0)
            {
                foreach (var product in productinfos.Item1)
                {
                    BatchInformation batchInfo = _batchInformationService.GetBatchInfoById(product.BatchId);
                    ProductInformationViewModel vm = new ProductInformationViewModel();
                    vm.Id = product.Id;
                    vm.Code = product.ProductCode;
                    vm.Name = product.ProductName;
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



        /// <summary>
        /// 新建商品页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult AddProduct(int id = 0)
        {
            ProductInformation productInfo = new ProductInformation();
            if (id > 0)
            {
                productInfo = _productInformationService.GetProductById(id);
            }
            List<BatchInformation> batchInfos = _batchInformationService.GetAll();
            ViewBag.BatchInfos = batchInfos;
            return View(productInfo);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DelProduct(int id)
        {
            try
            {
                _productInformationService.RemoveProductById(id);
                return Json(new { code = 1, msg = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "OK" });
            }
        }


        [HttpPost]
        public IActionResult SaveProduct()
        {
            try
            {
                int id = Request.Form["Id"].TryToInt(0);
                if (id > 0)
                {
                    var info = _productInformationService.GetProductById(id);
                    info.ProductCode = Request.Form["ProductCode"].TryToString();
                    info.ProductName = Request.Form["ProductName"].TryToString();
                    info.BatchId = Request.Form["BatchId"].TryToInt(0);
                    info.Type = (ClassTypeEnum)Request.Form["ClassType"].TryToInt(0);
                    info.ProductStatus = (ProductStatusTypeEnum)Request.Form["ProductStatusType"].TryToInt(0);
                    info.StockType = (StockStatusTypeEnum)Request.Form["StockStatusType"].TryToInt(0);
                    _productInformationService.UpdateProduct(info);
                    return Json(new { code = 1, msg = "OK", id = info.Id });
                }
                else
                {
                    ProductInformation productInfo = new ProductInformation();
                    productInfo.ProductCode = Request.Form["ProductCode"].TryToString();
                    productInfo.ProductName = Request.Form["ProductName"].TryToString();
                    productInfo.BatchId = Request.Form["BatchId"].TryToInt(0);
                    productInfo.Type = (ClassTypeEnum)Request.Form["ClassType"].TryToInt(0);
                    productInfo.ProductStatus = (ProductStatusTypeEnum)Request.Form["ProductStatusType"].TryToInt(0);
                    productInfo.StockType = (StockStatusTypeEnum)Request.Form["StockStatusType"].TryToInt(0);
                    id = _productInformationService.AddProduct(productInfo);
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
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = ex.Message });
            }
           
        }

        /// <summary>
        /// 商品图片页面
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public IActionResult AddProductPhotos(int ProductId)
        {
            var productInfo = _productInformationService.GetProductById(ProductId);
            return View(productInfo);
        }

        /// <summary>
        /// 图片关联
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Uploadattachment(int ProductId)
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
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + filename;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    imgFile.CopyTo(fs);
                    fs.Flush();
                }

                var productInfo = _productInformationService.GetProductById(ProductId);
                var details = new List<ProductDetail>();
                details.Add(new ProductDetail()
                {
                    ProductInformationId = ProductId,
                    PhotoPath = Path.Combine("img", uniqueFileName)
                });
                productInfo.Details = details;

                _productInformationService.UpdateProduct(productInfo);

                return Json(new { code = 0, msg = "上传成功", data = new { src = $"/img/{filePath}", title = "图片标题" } });
            }
            return Json(new { code = 1, msg = "上传失败", });
            #endregion
        }

        [HttpPost]
        public IActionResult SavePhotos()
        {
            int id = Request.Form["ID"].TryToInt(0);
            return Json(new { code = 1, msg = "OK", id = id });
        }

        /// <summary>
        /// 商品描述页面
        /// </summary>
        /// <returns></returns>
        public IActionResult AddProductDescription(int ProductId)
        {
            var productInfo = _productInformationService.GetProductById(ProductId);
            return View(productInfo);
        }


        /// <summary>
        ///  保存描述内容,图片中要显示绝对路径，不然前端渲染比较麻烦
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public IActionResult SaveProductDescriptionLog(int ProductId)
        {
            var productInfo = _productInformationService.GetProductById(ProductId);
            productInfo.Description = Request.Form["Rem"].TryToString();
            _productInformationService.UpdateProduct(productInfo);
            return Json(new { code = 1, msg = "OK" });
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


        private void PopulateProductStatusDropDownList(object selectedProductStatus = null)
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
            ViewBag.BatchInfos = batchInfos;
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

    }
}