using Colipu.BasicData.API.Application.Models.BasicDatas;
using Colipu.BasicData.API.Application.Services.BasicDatas;
using Colipu.BasicData.API.Application.Services.Redis;
using Colipu.BasicData.API.Core.BSystemDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Colipu.BasicData.API.Services
{
    public class AopService : IaopServicecs
    {
        public List<string> GetAllInformation()
        {
            return new List<string>() { "aop test" ,"this is test"};
        }

    }
}
