namespace SimpleLog
{
    public class MyLog : LogBase
    {

        public override bool LogAction(ILog LogClass)
        {
            log = LogClass;
            var LogMsg = $"[{this.CreateDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}][{this.Type.ToString()}] Message: {this.Msg}";
            log.Log(LogMsg, FileName, CreateDate);
            return true;
        }
        public MyLog(string msg, string[] FilePaths, LogType type = LogType.Info, string FileName = "Other") : base(msg, FilePaths, type, FileName) { }
        public MyLog(string msg, LogType type = LogType.Info, string FileName = "Other") : base(msg, null, type, FileName) { }
    }
}