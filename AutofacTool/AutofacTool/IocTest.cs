using System;
using System.Collections.Generic;
using Autofac;

namespace AutofacTool
{
    public class IocTest
    {
        AutofacConfig Autofac = new ExAutofac();
        IDataBase _database;
        public IocTest()
        {           
            Resolve();//解析
        }
        public void Resolve()
        {
            using (var scope = Autofac.Container.BeginLifetimeScope())
            {
                //如果你不清楚一个服务是否被注册了, 你可以通过 ResolveOptional() 或 TryResolve() 尝试解析: TryResolve暂未找到传参
                _database = scope.ResolveOptional<IDataBase>(new NamedParameter("type", "Test_"));

                //if (scope.TryResolve<IDataBase>(out _database))
                //{
                //    // Do something with the resolved provider value.
                //}

            }
        }
        public void Create()
        {
            _database.Create();
        }
        public void Delete()
        {
            _database.Delete();
        }
    }
}
