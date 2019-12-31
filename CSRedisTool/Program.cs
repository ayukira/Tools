using System;

namespace CSRedisTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            //初始化Redis链接
            MyRedis.Init();
            var model = new DataExample { Id = 1, Name = "cyan", Time = DateTime.Now };
            RedisExample.SetHash("modelKey", model);
            //while (true) 
            //{
            //    var model = RedisExample.Test();
            //    Console.WriteLine(model.Time.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            //    Console.WriteLine(model.Name);
            //}
            var model2 = RedisExample.GetHash<DataExample>("modelKey");
            RedisExample.HashMDel("modelKey");
            PrintModel(model2);
            Console.ReadLine();
        }
        public static void PrintModel<T>(T model)
        {
            if (model == null) return;
            foreach (var p in model.GetType().GetProperties())
            {
                Console.WriteLine($"{p.Name} : {p.GetValue(model)}");
            }
        }
    }
}