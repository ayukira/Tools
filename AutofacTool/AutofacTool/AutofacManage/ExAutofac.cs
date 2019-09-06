using System;
using Autofac;

namespace AutofacTool
{
    public class ExAutofac : AutofacConfig
    {
        public override void Register(ContainerBuilder builder)
        {
            builder.InstallServer<IDataBase>("AutofacTool.SqlDataBase,AutofacTool", InstanceType.PerDependency);
        }
    }
}
