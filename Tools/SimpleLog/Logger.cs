using System;
using System.IO;

namespace SimpleLog
{
    #region 写入日志

    /// <summary>
    /// 单次日一个IO流
    /// </summary>
    public class Logger : ILog
    {
        static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        static StreamWriter sw = null;
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="msg"></param>
        public void Log(string msg, string FileName, DateTime logDate)
        {
            DateTime dt = logDate;
            string dtf = dt.ToString("yyyyMM");
            try
            {
                string path = BasePath;
                string filePath = path + "\\Log\\" + FileName;
                string fileName = $"{dtf}.log";
                string totalPath = Path.Combine(filePath, fileName);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                sw = File.AppendText(totalPath);

                string logMsg = msg;

                sw.WriteLine(logMsg);
            }
            catch { }
            finally
            {
                if (sw != null)
                {
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
        public void Close() { }
        public void SetBasePath(string basePath)
        {
            BasePath = basePath;
        }
    }
    /// <summary>
    /// 多次连续日志一个IO流
    /// </summary>
    public class Logger2 : ILog, IDisposable
    {
        string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        StreamWriter sw = null;
        string PahtTemp = "";
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="msg"></param>
        public void Log(string msg, string FileName, DateTime logDate)
        {

            DateTime dt = logDate;
            string dtf = dt.ToString("yyyyMM");
            try
            {
                string path = BasePath;
                string filePath = path + "\\Log\\" + FileName;
                string fileName = $"{dtf}.log";
                string totalPath = Path.Combine(filePath, fileName);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (filePath != PahtTemp)
                {
                    PahtTemp = filePath;
                    Close();
                    sw = File.AppendText(totalPath);
                }
                string logMsg = msg;
                sw.WriteLine(logMsg);
            }
            catch { }
        }

        public void Close()
        {
            if (sw != null)
            {
                sw.Flush();
                sw.Close();
                sw.Dispose();
                PahtTemp = "";
            }
        }
        public void SetBasePath(string basePath)
        {
            BasePath = basePath;
        }
        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    Close();
                }
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                disposedValue = true;
            }
        }
        // 添加此代码以正确实现可处置模式。
        void IDisposable.Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    #endregion
}