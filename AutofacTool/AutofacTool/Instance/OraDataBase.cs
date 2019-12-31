using System;

namespace AutofacTool
{
    public class OraDataBase : IDataBase
    {
        public void Create()
        {
            Console.WriteLine("OraCreate");
        }
        public void Delete()
        {
            Console.WriteLine("OraDelete");
        }
    }
}