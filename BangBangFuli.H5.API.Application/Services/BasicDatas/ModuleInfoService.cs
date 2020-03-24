using System;
using System.Collections.Generic;
using System.Text;
using BangBangFuli.H5.API.Core.Entities;
using BangBangFuli.H5.API.Core.IRepositories.BasicDatas;

namespace BangBangFuli.H5.API.Application.Services.BasicDatas
{
    public class ModuleInfoService: IModuleInfoService
    {
        private readonly IModuleInfoRepository _moduleInfoRepository;

        public ModuleInfoService(IModuleInfoRepository moduleInfoRepository)
        {
            _moduleInfoRepository = moduleInfoRepository;
        }

        public List<ModuleInfo> GetList()
        {
            return _moduleInfoRepository.GetList();
        }

        public int AddModuleInfo(ModuleInfo module)
        {
            return _moduleInfoRepository.AddModuleInfo(module);
        }

        public void DelModel(ModuleInfo module)
        {
            _moduleInfoRepository.DelModel(module);
        }

        public ModuleInfo Get(int id)
        {
            return _moduleInfoRepository.Get(id);
        }
    }
}
