using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tools;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dictionary<string, int> dic = new Dictionary<string, int>();
            //dic.Add("test", 1);
            //dic.Add("test2", 2);
            //SimpleCache.Instance.Set("dic", dic, 60 * 60 * 1000);

            //var ab = SimpleCache.Instance.Get("dic");
            //Console.WriteLine(SimpleJson.ObjectToJson(dic));
            //Console.WriteLine(SimpleJson.ObjectToJson(ab));
            Console.WriteLine(DateTime.Today.ToLocalTime());
            Console.WriteLine(DateTime.Now.ToLocalTime().ToString());
            Console.ReadLine();
        }

    }
}