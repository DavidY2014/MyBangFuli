using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Controllers.Dtos;
using BangBangFuli.API.MVCDotnet2.Extensions;
using BangBangFuli.Common;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using BangBangFuli.H5.API.Core;
using BangBangFuli.H5.API.Core.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BangBangFuli.API.MVCDotnet2.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    public class BasicDataController : ControllerBase
    {
        private readonly ICouponService _couponService;
        private readonly IBannerService _bannerService;
        private readonly IProductInformationService _productService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductDetailService _productDetailService;
        private readonly ITransactionService _transactionService;
        private readonly IOrderService _orderService;

        private readonly ILogger _logger;
        public string VERFIY_CODE_TOKEN_COOKIE_NAME = "VerifyCode";

        public BasicDataController(ICouponService couponService, IBannerService bannerService, IProductInformationService productService, IOrderService orderService,
            IOrderDetailService orderDetailService, IProductDetailService productDetailService, ITransactionService transactionService, ILogger<BasicDataController> logger)
        {
            _couponService = couponService;
            _bannerService = bannerService;
            _orderService = orderService;
            _productService = productService;
            _orderDetailService = orderDetailService;
            _productDetailService = productDetailService;
            _transactionService = transactionService;
            _logger = logger;
        }



        /// <summary>
        /// 1,获取批次号获取banner图片数组,http://www.bangbangfuli.cn:5001/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/v1/BasicData/Banner/{batchId}")]
        public ResponseOutput GetBannerByBatchId(int batchId)
        {
            try
            {
                _logger.LogInformation($"进入GetBannerByBatchId方法 {batchId}");
                var photoUniqueNames = new List<string>();
                List<Banner> banners = _bannerService.GetBannersByBatchId(batchId);
                foreach (var item in banners)
                {
                    var bannerDetails = item.BannerDetails;
                    if (bannerDetails != null)
                    {
                        var bannerPhotos = new List<string>();
                        foreach (var detail in bannerDetails) {
                            var rawPhoto = Path.Combine("http://www.bangbangfuli.cn:5001/banners", detail.PhotoPath);
                            var processPhoto = rawPhoto.Replace('\\', '/');
                            bannerPhotos.Add(processPhoto);
                        }
                        photoUniqueNames.AddRange(bannerPhotos);
                    }
                }
                return new ResponseOutput(photoUniqueNames, "0", string.Empty, HttpContext.TraceIdentifier);
            }
            catch (Exception ex)
            {
                _logger.LogError($"异常是{ex.ToString()}");
                return new ResponseOutput(null, "-1", ex.Message, HttpContext.TraceIdentifier);
            }

        }


        /// <summary>
        ///2, 验证码接口,验证code写入cookie的VerifyCode键值对中
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/v1/BasicData/NumberVerifyCode")]
        public FileContentResult GetNumberVerifyCode()
        {
            try
            {
                _logger.LogInformation("进入GetNumberVerifyCode方法");
                string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.NumberVerifyCode);
                Response.Cookies.Append(VERFIY_CODE_TOKEN_COOKIE_NAME, code);
                byte[] codeImage = VerifyCodeHelper.GetSingleObj().CreateByteByImgVerifyCode(code, 100, 40);
                return File(codeImage, @"image/jpeg");
            }
            catch (Exception ex)
            {
                _logger.LogError($"异常为{ex.ToString()}");
                return null;
            }

        }

        /// <summary>
        ///2.1, 验证码接口,验证code写入字典
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/v1/BasicData/NumberVerifyCodeByDictionary")]
        public ResponseOutput GetNumberVerifyCodeByDictionary()
        {
            try
            {
                _logger.LogInformation("进入GetNumberVerifyCodeByDictionary方法");
                Dictionary<string, object> result = new Dictionary<string, object>();
                string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.NumberVerifyCode);
                byte[] codeImage = VerifyCodeHelper.GetSingleObj().CreateByteByImgVerifyCode(code, 100, 40);
                string imageStr = Convert.ToBase64String(codeImage);
                result.Add("VerifyCode", code);
                result.Add("ImageBase64", imageStr);
                return new ResponseOutput(result, "0", "Base64验证码", HttpContext.TraceIdentifier);
            }
            catch (Exception ex)
            {
                _logger.LogError($"异常为{ex.ToString()}");
                return new ResponseOutput(null, "-1", ex.Message, HttpContext.TraceIdentifier);
            }

        }



        /// <summary>
        /// 4,通过批次号获取下面所有商品,枚举备注
        /// ClassType:商品大类       
        /// yuexiangmeiwei=0, 悦享美味
        /// jujiahaowu=1,  居家好物
        /// pingzhishenghuo=2, 品质生活
        /// chufangzhengxuan=3,  厨房甑选
        /// unknown=4 ,未知类别
        /// -----------------------
        /// StockStatus:库存状态
        /// No=0,  没有货
        /// Yes=1,  有货
        /// Unknown=2，未知状态
        /// -----------------------
        /// ProductStatus:商品状态
        /// Down=0, 未上架
        /// On=1, 上架
        /// Unknown=2，未知状态 
        /// 
        /// 
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/v1/BasicData/BatchProducts/{batchId}")]
        public ResponseOutput GetProductsByBatchId(int batchId)
        {
            List<ProductDto> productDtos = new List<ProductDto>();
            List<ProductInformation> products = _productService.GetProductsByBatchId(batchId);

            try
            {
                _logger.LogInformation("进入GetProductsByBatchId方法");
                foreach (var product in products)
                {
                    //过滤商品状态
                    //if (product.ProductStatus == ProductStatusTypeEnum.Down || product.ProductStatus == ProductStatusTypeEnum.Unknown
                    //    || product.StockType == StockStatusTypeEnum.No || product.StockType == StockStatusTypeEnum.Unknown)
                    //{
                    //    continue;
                    //}
                    //过滤库存状态
                    if (product.ProductStatus == ProductStatusTypeEnum.Down || product.ProductStatus == ProductStatusTypeEnum.Unknown)
                    {
                        continue;
                    }
                    //图片详情
                    var productDetails = _productDetailService.GetDetailsByProductId(product.Id);
                    List<ProductDetailOutputDto> detailDtos = new List<ProductDetailOutputDto>();
                    if (productDetails != null)
                    {
                        foreach (var productDetail in productDetails)
                        {
                            detailDtos.Add(new ProductDetailOutputDto()
                            {
                                PhotoPath = productDetail.PhotoPath.Replace('\\', '/')
                            }) ;
                        }
                    }

                    var productDto = new ProductDto();
                    productDto.Id = product.Id;
                    productDto.Code = product.ProductCode;
                    productDto.Name = product.ProductName;
                    if (!string.IsNullOrEmpty(product.Description)) {
                        //修改富文本的图片为绝对路径
                        string processDescription = TextParse.ProcessHtmlImageUrlList(product.Description);
                        productDto.Description = processDescription;
                    }
                    productDto.StockStatus = (int)product.StockType;
                    productDto.ClassType = (int)product.Type;
                    productDto.ProductStatus = (int)product.ProductStatus;
                    productDto.Photos = detailDtos.Select(item => Path.Combine("http://www.bangbangfuli.cn:5001/", item.PhotoPath.Replace('\\', '/'))).ToList();
                    productDtos.Add(productDto);
                }
                return new ResponseOutput(productDtos, "0", string.Empty, HttpContext.TraceIdentifier);
            }
            catch (Exception ex)
            {
                _logger.LogError($"异常为{ex.ToString()}");
                return new ResponseOutput(null, "-1", ex.Message, HttpContext.TraceIdentifier);
            }


        }

        /// <summary>
        /// 根据id 获取商品信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/v1/BasicData/ProductDetail/{productId}")]
        public ResponseOutput GetProductDetailByProductId(int productId)
        {
            try
            {
                _logger.LogInformation("进入GetProductDetailByProductId方法");
                ProductInformation product = _productService.GetProductById(productId);
                //图片详情
                var productDetails = _productDetailService.GetDetailsByProductId(product.Id);
                List<ProductDetailOutputDto> detailDtos = new List<ProductDetailOutputDto>();
                if (productDetails != null)
                {
                    foreach (var productDetail in productDetails)
                    {
                        detailDtos.Add(new ProductDetailOutputDto()
                        {
                            PhotoPath = productDetail.PhotoPath.Replace('\\','/')
                        });
                    }
                }

                ProductDto dto = new ProductDto();
                dto.Code = product.ProductCode;
                dto.Name = product.ProductName;
                if (!string.IsNullOrEmpty(product.Description))
                {
                    //修改富文本的图片为绝对路径
                    string processDescription = TextParse.ProcessHtmlImageUrlList(product.Description);
                    dto.Description = processDescription;
                }
                dto.ClassType = (int)product.Type;
                dto.StockStatus = (int)product.StockType;
                dto.ProductStatus = (int)product.ProductStatus;
                dto.Photos = detailDtos.Select(item => Path.Combine("http://www.bangbangfuli.cn/", item.PhotoPath.Replace('\\', '/'))).ToList();
                
                return new ResponseOutput(dto, "0", string.Empty, HttpContext.TraceIdentifier);
            }
            catch (Exception ex)
            {
                _logger.LogError($"异常为{ex.ToString()}");
                return new ResponseOutput(null, "-1", ex.Message, HttpContext.TraceIdentifier);
            }
        }


        /// <summary>
        ///  4and5 返回大类下的商品信息，包含图片，是否有货，详情等信息
        /// </summary>
        /// <param name="classType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/v1/BasicData/ClassTypeProducts/{classType}")]
        public ResponseOutput GetProductsByCatelog(ClassTypeEnum classType)
        {
            var productDtos = new List<ProductDto>();
            try
            {
                _logger.LogInformation("进入GetProductsByCatelog方法");
                var products = _productService.GetProductsByClassType(classType);
                foreach (var product in products)
                {
                    //图片详情
                    var productDetails = _productDetailService.GetDetailsByProductId(product.Id);
                    List<ProductDetailOutputDto> detailDtos = new List<ProductDetailOutputDto>();
                    foreach (var productDetail in productDetails)
                    {
                        detailDtos.Add(new ProductDetailOutputDto()
                        {
                            PhotoPath = productDetail.PhotoPath.Replace('\\','/')
                        });
                    }

                    productDtos.Add(new ProductDto
                    {
                        Code = product.ProductCode,
                        Name = product.ProductName,
                        Description = product.Description,
                        ClassType = (int)product.Type,
                        StockStatus = (int)product.StockType,
                        ProductStatus = (int)product.ProductStatus,
                        Photos = detailDtos.Select(item => Path.Combine("http://www.bangbangfuli.cn/", item.PhotoPath.Replace('\\','/'))).ToList()
                    });
                }

                return new ResponseOutput(productDtos, "0", string.Empty, HttpContext.TraceIdentifier);

            }
            catch (Exception ex)
            {
                _logger.LogError($"异常为{ex.ToString()}");
                return new ResponseOutput(null, "-1", ex.Message, HttpContext.TraceIdentifier);
            }

        }

        /// <summary>
        /// 3,兑换,成功就返回券号信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/v1/BasicData/ExchangeCoupon")]
        public ResponseOutput ExchangeCoupon(CouponInputDto couponInputDto)
        {
            CouponDto dto = new CouponDto();
            try
            {
                _logger.LogInformation("进入ExchangeCoupon方法");
                //先判断券是否有效
                var ret = _couponService.VerifyCoupon(couponInputDto.Code, couponInputDto.Password);
                if (ret)
                {
                    var coupon = _couponService.GetCouponByCode(couponInputDto.Code);
                    //判断此券是不是此批次号
                    if (coupon.BatchId != couponInputDto.BatchId) {
                        return new ResponseOutput(null, "-1", $"此券不属于批次号{ couponInputDto.BatchId}", HttpContext.TraceIdentifier);
                    }
                    dto = new CouponDto
                    {
                        Code = coupon.Code,
                        BatchId = coupon.BatchId,
                        ValidityDate = coupon.ValidityDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        AvaliableCount = coupon.AvaliableCount,
                        TotalCount = coupon.TotalCount
                    };
                    //判断是否过期 t1<t2
                    if (DateTime.Compare(coupon.ValidityDate, DateTime.Now) < 0)
                    {
                        dto.IsOutDate = true;
                        return new ResponseOutput(dto, "-1", "此券已过期", HttpContext.TraceIdentifier);
                    }
                    else
                    {
                        dto.IsOutDate = false;
                    }
                    if (coupon.AvaliableCount <= 0)
                    {
                        return new ResponseOutput(dto, "0", "此券次数已用完", HttpContext.TraceIdentifier);
                    }
                    return new ResponseOutput(dto, "0", "兑换成功，可用次数为" + coupon.AvaliableCount--, HttpContext.TraceIdentifier);
                }
                else
                {
                    return new ResponseOutput(null, "-1", "劵信息获取失败", HttpContext.TraceIdentifier);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"异常为{ex.ToString()}");
                return new ResponseOutput(null, "-1", ex.Message, HttpContext.TraceIdentifier);
            }

        }


        /// <summary>
        /// 6,下订单，可以传入多个商品的信息
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/v1/BasicData/NewOrder")]
        public ResponseOutput CreateNewOrder(OrderInputDto inputDto)
        {
            try
            {
                _logger.LogInformation("进入CreateNewOrder方法");
                if (inputDto == null)
                {
                    return new ResponseOutput(null, "-1", "传入参数为空", HttpContext.TraceIdentifier);
                }

                if (inputDto.CouponCode == "0")
                {
                    return new ResponseOutput(null, "-1", "券号不对", HttpContext.TraceIdentifier);
                }

                var coupon = _couponService.GetCouponByCode(inputDto.CouponCode);
                if (coupon == null)
                {
                    return new ResponseOutput(null, "-1", "券不存在", HttpContext.TraceIdentifier);
                }
                //判断券的次数，不够的话就不能下订单
                if (coupon.AvaliableCount <= 0)
                {
                    return new ResponseOutput(null, "-1", "此券次数已用完,不能下单", HttpContext.TraceIdentifier);
                }

                List<OrderDetail> details = new List<OrderDetail>();

                foreach (var item in inputDto.DetailDtos)
                {
                    ProductInformation productInfo = _productService.GetProductById(item.ProductId);

                    details.Add(new OrderDetail
                    {
                        ProductId = item.ProductId,
                        ProductCode = productInfo.ProductCode,
                        ProductName = productInfo.ProductName,
                        ProductCount = item.Count,
                    });
                }


                long currentTicks = DateTime.Now.Ticks;
                DateTime dtFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                long currentMillis = (currentTicks - dtFrom.Ticks) / 10000;
                Order order = new Order
                {
                    OrderCode = currentMillis.ToString(),//订单号用随机数
                    CreateTime = DateTime.Now,
                    CouponCode = inputDto.CouponCode,
                    Contactor = inputDto.Contactor,
                    MobilePhone = inputDto.MobilePhone,
                    Province = inputDto.Province,
                    City = inputDto.City,
                    District = inputDto.District,
                    Address = inputDto.Address,
                    ZipCode = int.Parse(inputDto.ZipCode),
                    Telephone = inputDto.Telephone,
                    Details = details
                };

                #region 事务处理

                var ret = _transactionService.CreateNewOrderTransaction(order, coupon);

                #endregion

                //发送手机短信给用户，当然这个可以用job实现
                if (ret)
                    return new ResponseOutput(null, "0", "创建订单成功", HttpContext.TraceIdentifier);

                return new ResponseOutput(null, "-1", "创建订单失败", HttpContext.TraceIdentifier);
            }
            catch (Exception ex)
            {
                _logger.LogError($"异常为{ex.ToString()}");
                return new ResponseOutput(null, "-1", ex.Message, HttpContext.TraceIdentifier);
            }

        }


        /// <summary>
        /// 7和8 获取此兑换券生成的订单列表,入参为券卡号
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/v1/BasicData/Orders/{couponCode}")]
        public ResponseOutput GetOrderList(string couponCode)
        {
            try
            {
                _logger.LogInformation("进入GetOrderList方法");
                List<OrderOutputDto> orderDtos = new List<OrderOutputDto>();
                Coupon coupon = _couponService.GetCouponByCode(couponCode);
                List<Order> orders = _orderService.GetOrdersByCoupon(coupon.Code);
                foreach (var order in orders)
                {
                    List<OrderDetail> details = _orderDetailService.GetOrderDetailByOrderId(order.Id);

                    List<OrderDetailOutputDto> detailOutputDtos = new List<OrderDetailOutputDto>();
                    foreach (var detail in details)
                    {
                        var productDetails = _productDetailService.GetDetailsByProductId(detail.ProductId);


                        detailOutputDtos.Add(new OrderDetailOutputDto
                        {
                            ProductId = detail.ProductId,
                            ProductCode = detail.ProductCode,
                            ProductName = detail.ProductName,
                            ProductCount = detail.ProductCount,
                            ProductImageUrl = Path.Combine("http://www.bangbangfuli.cn:5001/",productDetails?.First().PhotoPath.Replace('\\', '/'))
                        });
                    }

                    orderDtos.Add(new OrderOutputDto
                    {
                        OrderCode = order.OrderCode,
                        CouponCode = couponCode,
                        Contactor = order.Contactor,
                        MobilePhone = order.MobilePhone,
                        Province = order.Province,
                        City = order.City,
                        District = order.District,
                        Address = order.Address,
                        ZipCode = order.ZipCode,
                        Telephone = order.Telephone,
                        CreateTime = order.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        DeliveryNumber = order.DeliveryNumber,
                        Details = detailOutputDtos
                    });
                }

                return new ResponseOutput(orderDtos, "0", string.Empty, HttpContext.TraceIdentifier);
            }
            catch (Exception ex)
            {
                _logger.LogError($"异常为{ex.ToString()}");
                return new ResponseOutput(null, "-1", ex.Message, HttpContext.TraceIdentifier);
            }

        }

    }
}