using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.H5.API.Application.Services.Redis
{
    public interface ICacheService
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);

        T Get<T>(string key);

        bool IsExists(string key);

       void Set(string key, string value, int expiredTime = 0);

        void Set<T>(string key, T value, int expiredTime = 0);

        void Remove(string key);

    }
}
