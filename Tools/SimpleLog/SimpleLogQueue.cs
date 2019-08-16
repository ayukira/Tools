using System;
using System.Collections.Concurrent;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SimpleLog
{
	#region 日志队列	
	public sealed class SimpleLogQueue: ISerializable
	{
		private static  readonly Lazy<SimpleLogQueue> instance = new Lazy<SimpleLogQueue>(() => { return new SimpleLogQueue(); }, true);
		private static ConcurrentQueue<LogBase> Logs = new ConcurrentQueue<LogBase>();
		private static bool IsRun = false;
		public void Log(string Msg,LogType Type,string Custom = "Other")
		{
			var log = new MyLog(Msg,Type, Custom);
			Logs.Enqueue(log);
			if (!IsRun)
			{
				IsRun = true;
				Run();
			}
		}
		private void Run()
		{
			if (Logs.IsEmpty)
			{
				IsRun = false;
				return;
			}
			else
			{
				Task.Run(() => {
					ILog logger = new Logger2();// 多次连续日志一个IO流
					//ILog logger = new Logger();  //一次日志一个IO流

					while (!Logs.IsEmpty)
					{
						Logs.TryDequeue(out LogBase log);
						if (log != null)
						{
							log.LogAction(logger);
						}
					}
					logger.Close();
					IsRun = false;
				});
			}
			return;
		}

		public static SimpleLogQueue Instance { get { return instance.Value; } }
		public static bool IsCreate { get { return instance.IsValueCreated; } }
		private SimpleLogQueue() { }
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new Exception("Disallow Deserialization");
		}
	}
	#endregion
}