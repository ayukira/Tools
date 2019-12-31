using System;

namespace AutofacTool
{
    public class SqlDataBase : IDataBase
    {
        string type = "type";
        public SqlDataBase() { }
        public SqlDataBase(string type)
        {
            this.type = type;
        }
        public void Create()
        {
            Console.WriteLine(type + "SqlCreate");
        }
        public void Delete()
        {
            Console.WriteLine(type + "SqlDelete");
        }
    }
}