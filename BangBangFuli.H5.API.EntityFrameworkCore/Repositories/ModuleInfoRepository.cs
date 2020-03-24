using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;
using BangBangFuli.Utils.ORM.Imp;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Repositories
{
    public class ModuleInfoRepository: BaseRepository<CouponSystemDBContext, ModuleInfo>, IModuleInfoRepository
    {
        public ModuleInfoRepository(CouponSystemDBContext dbContext) : base(dbContext)
        {

        }

        public List<ModuleInfo> GetList()
        {
            try
            {
                List<ModuleInfo> list = null;
                list = Master.ModuleInfos.ToList();
                return list;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public int AddModuleInfo(ModuleInfo module)
        {
            int id = 0;
            try
            {
                var moduleinfo =  Master.ModuleInfos.Add(module);
                Master.SaveChanges();
                id = moduleinfo.Entity.ID;
            }
            catch (Exception ex)
            {
            }
            return id;
        }

        public void DelModel(ModuleInfo module)
        {
            try
            {
                Master.ModuleInfos.Remove(module);
                Master.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public ModuleInfo Get(int id)
        {
            try
            {
                var info = Master.ModuleInfos.Find(id);
                return info;
            }
            catch (Exception ex)
            {
            }
            return null;
        }




    }
}
