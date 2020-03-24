using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.Utils.ORM.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BangBangFuli.H5.API.Core.IRepositories.BasicDatas
{
    public interface IProductRepository: IBaseRepository<ProductInformation>
    {
        List<ProductInformation> GetAll();

        void Save(ProductInformation product);

        int AddProduct(ProductInformation productInfo);

        List<ProductInformation> GetProductsByClassType(ClassTypeEnum type);
        void UpdateProduct(ProductInformation product);
        ProductInformation GetProductById(int ProductId);
        List<ProductInformation> GetProductsByBatchId(int batchId);

        void RemoveProductById(int productId);
    }
}
