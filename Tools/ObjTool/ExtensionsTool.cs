using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Tools
{
    public static class ObjExtensions
    {
        /// <summary>
        /// 简单Copy对象,只Copy公共可写属性，且对象有构造方法 
        /// </summary>
        /// <typeparam name="T">对象泛型</typeparam>
        /// <param name="obj">需要Copy的对象</param>
        /// <returns></returns>
        public static T SimpleCopy<T>(this T obj) where T : new()
        {
            T t = new T();
            foreach (var p in obj.GetType().GetProperties())
            {
                if (p.CanWrite)
                {
                    var name = p.Name;
                    var value = p.GetValue(obj, null);
                    t.GetType().GetProperty(name).SetValue(t, value);
                }
            }
            return t;
        }
        /// <summary>
        /// ForEach扩展,空处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        public static void ForEachEx<T>(this List<T> list,Action<T> action) 
        {
            if (list != null) 
            {
                list.ForEach(action);
            }
        }
    }

    public static class DateExtensions
    {
        /// <summary>
        /// 时间转 yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToChString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 时间转毫秒时间戳
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToTimeStamp(this DateTime date)
        {
            DateTime DateMin = new DateTime(1970, 1, 1);
            TimeSpan ts = date.ToLocalTime() - DateMin.ToLocalTime();
            return ts.Milliseconds;
        }
    }

    public static class StringExtensions
    {
        /// <summary>
        /// 计算字符串中单词数量,分割字符' ';'.';'?';';';',' 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chars">new char[] { ' ', '.', '?',';',','}</param>
        /// <returns></returns>
        public static int WordCount(this string str, char[] chars = null)
        {
            if (chars == null)
            {
                chars = new char[] { ' ', '.', '?', ';', ',' };
            }
            return str.Split(chars, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
    public static class ArrayExtensions
    {
        /// <summary>
        /// 数组转List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this T[] arr)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < arr.Length; i++)
            {
                list.Add(arr[i]);
            }
            return list;
        }
    }
    public static class ListExtensions {

        public static ConcurrentQueue<T> ToSaveQueue<T>(List<T> list) {
            ConcurrentQueue<T> queue = new ConcurrentQueue<T>();
            if (list != null) {
                list.ForEach(x => {
                    queue.Enqueue(x);
                });
            }
            return queue;            
        }
        public static ConcurrentBag<T> ToSaveBag<T>(List<T> list)
        {
            ConcurrentBag<T> bag = new ConcurrentBag<T>();
            if (list != null)
            {
                list.ForEach(x => {
                    bag.Add(x);
                });
            }
            return bag;
        }
        
    }
}
