using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public interface IModuleInfoService:IAppService
    {
        List<ModuleInfo> GetList();
        int AddModuleInfo(ModuleInfo module);
        void DelModel(ModuleInfo module);
        ModuleInfo Get(int id);
    }
}
