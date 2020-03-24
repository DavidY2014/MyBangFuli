using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BangBangFuli.H5.API.WebAPI
{
    public static class BulkInjection
    {
        public static void AddByAssembly(this IServiceCollection service, string assemblyName, string baseInterface, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            if (string.IsNullOrWhiteSpace(assemblyName)) return;

            List<Type> types;

            try
            {
                Assembly assembly = Assembly.Load(assemblyName);

                types = assembly.GetTypes().ToList();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("服务注册异常，请联系管理员！");
            }

            Dictionary<Type, Type[]> instanceInterfaces = new Dictionary<Type, Type[]>();

            foreach (var type in types.Where(s => !s.IsInterface))
            {
                Type[] interfaceType = type.GetInterfaces();

                if (interfaceType.Length == 0 || !interfaceType.Any(t => t.Name.Contains(baseInterface, StringComparison.OrdinalIgnoreCase))) continue;

                instanceInterfaces.Add(type, interfaceType);
            }

            foreach (var instanceInterface in instanceInterfaces)
            {
                foreach (var interfaceDefinition in instanceInterface.Value)
                {
                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Singleton:
                            service.AddSingleton(interfaceDefinition, instanceInterface.Key);
                            break;
                        case ServiceLifetime.Scoped:
                            service.AddScoped(interfaceDefinition, instanceInterface.Key);
                            break;
                        case ServiceLifetime.Transient:
                            service.AddTransient(interfaceDefinition, instanceInterface.Key);
                            break;
                        default:
                            service.AddScoped(interfaceDefinition, instanceInterface.Key);
                            break;
                    }
                }
            }
        }
    }
}
