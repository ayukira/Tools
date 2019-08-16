using System;
using System.IO;

namespace SimpleLog
{
	public abstract class LogBase
	{
		protected ILog log = null;
		public string Msg { get; set; }
		public LogType Type { get; set; } = LogType.Info;
		public DateTime CreateDate { get; set; } = DateTime.Now;
		public string FileName { get; set; }
		public abstract bool LogAction(ILog LogClass);
		public LogBase(string msg, string[] FilePaths, LogType type, string FileName)
		{
			this.Msg = msg;
			this.Type = type;
			string path = "";
			if (type == LogType.Other)
			{
				this.FileName = FileName;
			}
			else
			{
				this.FileName = type.ToString();
			}
			if (FilePaths != null && FilePaths.Length > 0)
			{
				path = Path.Combine(FilePaths);
				path = Path.Combine(path, FileName);
				FileName = path;
			}
		}
	}
}