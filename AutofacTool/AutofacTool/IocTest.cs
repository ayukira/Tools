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
                _database = scope.Resolve<IDataBase>(new NamedParameter("type", "Test_"));
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
