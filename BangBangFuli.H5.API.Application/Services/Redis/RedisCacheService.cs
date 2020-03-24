using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.H5.API.Application.Services.Redis
{
    public class RedisCacheService : ICacheService
    {

        private readonly RedisCache _redisCache;

        public RedisCacheService(RedisCacheOptions options)
        {
            _redisCache = new RedisCache(options);
        }

        private string GetString(string key)
        {
            var value = _redisCache.GetString(key);
            return value;
        }

        public string Get(string key)
        {
            return GetString(key);
        }

        public T Get<T>(string key)
        {
            var valueString = GetString(key);
            if (!string.IsNullOrEmpty(valueString))
            {
                return JsonConvert.DeserializeObject<T>(valueString);
            }
            return default(T);
        }

        public bool IsExists(string key)
        {
            if (string.IsNullOrEmpty(GetString(key)))
            {
                return false;
            }
            return true;
        }

        public void Remove(string key)
        {
            _redisCache.Remove(key);
        }

        public void Set(string key, string value, int expiredTime = 0)
        {
            _redisCache.Set(key, Encoding.UTF8.GetBytes(value), new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expiredTime)
            });
        }

        public void Set<T>(string key, T value, int expiredTime =0)
        {
            _redisCache.Set(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expiredTime)
            });
        }


    }
}
