using System;
using System.Runtime.InteropServices;

namespace Test.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) 
            {
                int nowStage = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(nowStage);
                switch (nowStage)
                {
                    case int i when (nowStage <= 5000):
                        Console.WriteLine(5000);
                        break;
                    case int i when (nowStage > 5000 && nowStage <= 10000):
                        Console.WriteLine(5001);
                        break;
                    case int i when (nowStage > 10000 && nowStage <= 50000):
                        Console.WriteLine(10001);
                        break;
                    case int i when (nowStage > 50000):
                        Console.WriteLine(50001);
                        break;
                    default:
                        Console.WriteLine(-1);
                        break;
                }
            }
            
            Console.WriteLine(DateTime.Now);
            //var json = SimpleJson.Inc.ObjectToJson(new { MyId = 1, MyName = "123",Date = DateTime.Now });

            //Console.WriteLine(json);
            //var test = new JsonConfiguration("Test");
            //while (true) 
            //{
            //    var code = test.Configuration.GetSection("TestCode").Value;
            //    Console.WriteLine("TestCode:" + code);
            //    System.Threading.Thread.Sleep(5000);
            //}
            //PlatformServices.Default.Application.RuntimeFramework.FullName
            //Microsoft.Extensions.DependencyModel.DependencyContext.Default
            Console.WriteLine(System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription);
            var dic = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            Console.WriteLine(dic);
            Tools.SimpleJson.Inc.ObjectToJson(dic);
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
        public static void Onchange() 
        {
            Console.WriteLine("Change2");
        }
    }
}
