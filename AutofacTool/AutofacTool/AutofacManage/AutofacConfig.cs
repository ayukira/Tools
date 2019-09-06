using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace AutofacTool
{
    public abstract class AutofacConfig
    {
        public IContainer Container { get; set; }
        public AutofacConfig()
        {
            var builder = new ContainerBuilder();
            Register(builder);
            Container = builder.Build();
        }
        /// <summary>
        /// IOC服务器注册
        /// </summary>
        public abstract void Register(ContainerBuilder builder);
    }
}
