using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public class ProductInformationService : IProductInformationService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductInformationService(IProductRepository productRepository,IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public List<ProductInformation> GetAll()
        {
           return   _productRepository.GetAll();
        }

        public void Save(ProductInformation product)
        {
            _productRepository.Save(product);
            _unitOfWork.SaveChanges();
        }

        public int AddProduct(ProductInformation productInfo)
        {
            return _productRepository.AddProduct(productInfo);
        }

        public void UpdateProduct(ProductInformation product)
        {
            _productRepository.UpdateProduct(product);
            _unitOfWork.SaveChanges();
        }

        public List<ProductInformation> GetProductsByClassType(ClassTypeEnum type)
        {
            return _productRepository.GetProductsByClassType(type);
        }

        public ProductInformation GetProductById(int ProductId)
        {
            return _productRepository.GetProductById(ProductId);
        }

        public List<ProductInformation> GetProductsByBatchId(int batchId)
        {
            return _productRepository.GetProductsByBatchId(batchId);
        }

        public void RemoveProductById(int productId)
        {
            _productRepository.RemoveProductById(productId);
            _unitOfWork.SaveChanges();
        }
    }
}
