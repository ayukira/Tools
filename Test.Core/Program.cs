using Microsoft.Extensions.Primitives;
using System;
using Tools;

namespace Test.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            //var json = SimpleJson.Inc.ObjectToJson(new { MyId = 1, MyName = "123",Date = DateTime.Now });

            //Console.WriteLine(json);
            var test = new JsonConfiguration("Test");
            //while (true) 
            //{
            //    var code = test.Configuration.GetSection("TestCode").Value;
            //    Console.WriteLine("TestCode:" + code);
            //    System.Threading.Thread.Sleep(5000);
            //}
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
        public static void Onchange() 
        {
            Console.WriteLine("Change2");
        }
    }
}
