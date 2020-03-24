using BangBangFuli.H5.API.Core;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public class SupplierService: ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(ISupplierRepository supplierRepository,IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }
        public List<Supplier> GetAll()
        {
            return _supplierRepository.GetAll();
        }

        public Supplier GetSupplierById(int supplierId)
        {
            return _supplierRepository.GetSupplierById(supplierId);
        }

        public void CreateNew(Supplier  supplier)
        {
            _supplierRepository.CreateNew(supplier);
            _unitOfWork.SaveChanges();
        }
    }
}
