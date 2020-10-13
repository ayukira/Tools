using System;
using System.Collections.Generic;
using System.Text;

namespace Tools
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回""
        /// </summary>
        /// <param name="input">输入值</param>
        public static string ToSafeString(this object input)
        {
            return input == null ? string.Empty : input.ToString().Trim();
        }
        /// <summary>
        /// 简单Copy对象,只Copy公共可写属性，且对象有公共构造方法 
        /// </summary>
        /// <typeparam name="T">对象泛型</typeparam>
        /// <param name="obj">需要Copy的对象</param>
        /// <returns></returns>
        public static T SimpleCopy<T>(this T obj) where T : new()
        {
            T t = new T();
            foreach (var p in obj.GetType().GetProperties())
            {
                if (p.CanWrite && p.CanRead)
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
        public static void ForEachEx<T>(this List<T> list, Action<T> action)
        {
            list?.ForEach(action);
        }
    }
}
