using System;

namespace AutofacTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var ioc = new IocTest();
            ioc.Create();

            Console.ReadLine();
        }
    }
}