using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BangBangFuli.H5.API.WebAPI.Extensions
{
    public static class AutofacExtension
    {
        public static Type[] AddFilterTypes(this ContainerBuilder builder,string assemblyName, string cacheBaseInterface)
        {
            if (string.IsNullOrWhiteSpace(assemblyName)) return null;

            Type[] types;
            List<Type> instanceInterfaces = new List<Type>(); 
            Assembly assembly = Assembly.Load(assemblyName);

            types = assembly.GetTypes();

            foreach (var type in types.Where(s => !s.IsInterface))
            {
                Type[] interfaceType = type.GetInterfaces();

                if (interfaceType.Length == 0 || !interfaceType.Any(t => t.Name.Contains(cacheBaseInterface, StringComparison.OrdinalIgnoreCase))) continue;

                instanceInterfaces.Add(type);
            }

            return instanceInterfaces.ToArray();
        }
    }
}
