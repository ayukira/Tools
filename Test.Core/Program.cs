using System;
using Tools;

namespace Test.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = SimpleJson.Inc.ObjectToJson(new { MyId = 1, MyName = "123",Date = DateTime.Now });
            Console.WriteLine(json);
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
