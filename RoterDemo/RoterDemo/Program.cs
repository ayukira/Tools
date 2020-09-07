using System;

namespace RoterDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MyRoter.BuildRoter();
            string printMsg = "Test Msg";
            while (true) 
            {
                var route = Console.ReadLine();
                if (!MyRoter.ExistsRoute(route)) {
                    Console.WriteLine($"Router [{route}] not exists");
                    continue;
                }
                var roterInfo = MyRoter.GetRouterInfo(route);
                if (roterInfo == null) { continue; }
                var ins = MyRoter.GetInstance(roterInfo.ClassFullName);
                if(ins ==null) 
                {
                    ins = MyRoter.CreateInstance(roterInfo.ClassType);
                    MyRoter.AddInstance(roterInfo.ClassFullName, ins);
                }
                object result = roterInfo.methodInfo.Invoke(ins, new object[] { printMsg });
            }
        }
    }
}
