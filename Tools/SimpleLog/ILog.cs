using System;
namespace SimpleLog
{
    public interface ILog
    {
        void Log(string msg, string FileName, DateTime logDate);
        void Close();
        void SetBasePath(string basePath);
    }
}