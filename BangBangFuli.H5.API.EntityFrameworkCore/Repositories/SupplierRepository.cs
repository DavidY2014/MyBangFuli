using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using BangBangFuli.Utils.ORM.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Repositories
{
    public class SupplierRepository: BaseRepository<CouponSystemDBContext, Supplier>, ISupplierRepository
    {

        public SupplierRepository(CouponSystemDBContext dbContext):base(dbContext)
        {

        }

        public List<Supplier> GetAll()
        {
            return Master.Suppliers.ToList();
        }

        public Supplier GetSupplierById(int supplierId)
        {
            return Master.Suppliers.Find(supplierId);
        }

        public void CreateNew(Supplier supplier)
        {
            Master.Suppliers.Add(supplier);
        }

    }
}
