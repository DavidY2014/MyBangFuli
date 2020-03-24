using Castle.DynamicProxy;
using BangBangFuli.H5.API.Application.Models.BasicDatas;
using BangBangFuli.H5.API.Application.Services.Redis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangBangFuli.H5.API.WebAPI.AOP
{
    public class CachingIntercept : IInterceptor
    {
        //private  ICache _cache;
        //public CachingIntercept(ICache cache)
        //{
        //    //_cache = cache;
        //}
        public void Intercept(IInvocation invocation)
        {
            ////获取自定义缓存键
            //var cacheKey = CustomCacheKey(invocation);
            //var cacheValue = _cache.Get(cacheKey);
            //if (cacheValue != null)
            //{
            //    //返回的类型转换
            //    var type = invocation.Method.ReturnType;
            //    var resultTypes = type.GenericTypeArguments;
            //    if (type.FullName == "System.Void")
            //    {
            //        return;
            //    }
            //    object response = Convert.ChangeType(_cache.Get<object>(cacheKey), type);
            //    invocation.ReturnValue = response;
            //    return;
            //}
            //invocation.Proceed();
            //if (!string.IsNullOrWhiteSpace(cacheKey))
            //{
            //    object response;
            //    var type = invocation.Method.ReturnType;
            //    if (typeof(Task).IsAssignableFrom(type))
            //    {
            //        var resultProperty = type.GetProperty("Result");
            //        response = resultProperty.GetValue(invocation.ReturnValue);
            //    }
            //    else
            //    {
            //        response = invocation.ReturnValue;
            //    }

            //    if (response != null)
            //    {
            //        _cache.Set(cacheKey, response, new TimeSpan(0, 10, 0));
            //    }
            //}
        }

        //自定义缓存键
        private string CustomCacheKey(IInvocation invocation)
        {
            var typeName = invocation.TargetType.Name;
            var methodName = invocation.Method.Name;
            var methodArguments = invocation.Arguments.Select(GetArgumentValue).Take(3).ToList();//获取参数列表，我最多需要三个即可

            string key = $"{typeName}:{methodName}:";
            foreach (var param in methodArguments)
            {
                key += $"{param}:";
            }

            return key.TrimEnd(':');
        }


        //object 转 string
        private string GetArgumentValue(object arg)
        {
            if (arg is int || arg is long || arg is string)
                return arg.ToString();

            if (arg is DateTime)
                return ((DateTime)arg).ToString("yyyyMMddHHmmss");

            return "";
        }
    }
}
