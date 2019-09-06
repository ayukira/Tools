using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace AutofacTool
{
    public static class ContainerExtensions
    {
        public static ContainerBuilder InstallServer<T>(this ContainerBuilder containerBuilder, string TypeName, InstanceType ins = (InstanceType)1) where T : class
        {
            object obj = CreateInstance(TypeName);
            if (obj != null)
            {
                switch (ins)
                {
                    case InstanceType.PerDependency:
                        containerBuilder.RegisterTypes(obj.GetType()).As<T>().InstancePerDependency();
                        break;
                    case InstanceType.PerLifetimeScope:
                        containerBuilder.RegisterTypes(obj.GetType()).As<T>().InstancePerLifetimeScope();
                        break;
                    case InstanceType.Single:
                        containerBuilder.RegisterTypes(obj.GetType()).As<T>().SingleInstance();
                        break;
                    default:
                        containerBuilder.RegisterTypes(obj.GetType()).As<T>();
                        break;
                }
            }
            return containerBuilder;
        }
        public static object CreateInstance(string ClassName)
        {
            object obj = null;
            try
            {
                var str = ClassName.Split(',');
                var ass = System.Reflection.Assembly.Load(str[1]);
                obj = ass.CreateInstance(str[0]);
            }
            catch { }
            return obj;
        }
    }
}
