using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Confluent.Kafka;

namespace Confluent.Kafka.Tool
{
    public class KafkaCli
    {
        private readonly static string mTopick = "testtopick";
        private readonly static string mBootstrapServers = "hostname:9092";
        public static void Producer()
        {
            var config = new ProducerConfig { BootstrapServers = mBootstrapServers };
            Action<DeliveryReport<Null, string>> handler = r =>
            Console.WriteLine(!r.Error.IsError
                ? $"Delivered message to {r.TopicPartitionOffset}"
                : $"Delivery Error: {r.Error.Reason}");
            var producerBuilder = new ProducerBuilder<Null, string>(config);
            // 错误日志监视
            producerBuilder.SetErrorHandler((p, msg) =>
            {
                Console.WriteLine($"Producer_Erro信息：Code：{msg.Code}；Reason：{msg.Reason}；IsError：{msg.IsError}");
            });

            using (var producer = producerBuilder.Build())
            {
                for (int i = 0; i < 5; i++)
                {
                    // 异步发送消息到主题
                    producer.Produce(mTopick, new Message<Null, string> { Value = i.ToString() }, handler);
                }
                // 3后 Flush到磁盘
                producer.Flush(TimeSpan.FromSeconds(3));
            }
        }
        public static void Customer()
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group-2",
                BootstrapServers = mBootstrapServers,
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                c.Subscribe(mTopick);

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);
                            Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }
            }
        }
    }
}
