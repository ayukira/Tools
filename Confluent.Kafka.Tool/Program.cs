using System;

namespace Confluent.Kafka.Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //生产者
            KafkaCli.Producer();
            //消费者
            KafkaCli.Customer();
            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
