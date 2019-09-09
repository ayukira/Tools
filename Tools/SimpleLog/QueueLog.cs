using SimpleLog;
/// <summary>
///  简单日志队列 yzm 2019/06/25
///  日志处理速度需要优化，如IO批量处理，同时写入多个日志，减少IO流的频繁打开关闭 
/// </summary>
namespace Tools
{
    #region 调用日志方法
    /// <summary>
    /// 调用日志
    /// </summary>
    public class QueueLog
    {
        private static SimpleLogQueue log = SimpleLogQueue.Instance;
        
        public static void Info(string msg)
        {
            log.Log(msg, LogType.Info);
        }
		public static void Sql(string msg)
		{
			log.Log(msg, LogType.SQL);
		}
		public static void Debug(string msg) {
			log.Log(msg, LogType.DeBug);
		}
		public static void Warn(string msg)
		{
			log.Log(msg, LogType.Warn);
		}
		public static void Error(string msg)
        {
            log.Log(msg, LogType.Error);
        }
        public static void Custom(string msg, string FlieName)
        {
            log.Log(msg, LogType.Other, FlieName);
        }
        public static void SetLogger(ILog logger) {
            log.SetLogger(logger);
        }
    }

    #endregion
}