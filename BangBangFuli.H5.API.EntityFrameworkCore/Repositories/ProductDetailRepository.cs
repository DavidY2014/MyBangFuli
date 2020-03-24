using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using BangBangFuli.Utils.ORM.Imp;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Repositories
{
   public class ProductDetailRepository: BaseRepository<CouponSystemDBContext, ProductDetail>, IProductDetailRepository
    {

        public ProductDetailRepository(CouponSystemDBContext dbContext):base(dbContext)
        {

        }

        public List<ProductDetail> GetDetailsByProductId(int productId)
        {
            return Master.ProductDetails.Where(item => item.ProductInformationId == productId).ToList();
        }

    }
}
