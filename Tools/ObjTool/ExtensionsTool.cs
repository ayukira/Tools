using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
        public static void ForEachEx<T>(this List<T> list, Action<T> action)
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
            if (arr == null) return null;
            List<T> list = new List<T>();
            for (int i = 0; i < arr.Length; i++)
            {
                list.Add(arr[i]);
            }
            return list;
        }
        public static T[] ArrayAdd<T>(this T[] arr1, T[] arr2)
        {
            if (arr1 == null) return arr1;
            if (arr2 == null) return arr1;
            if (arr2.Length <= 0) return arr1;
            int index = arr1.Length + arr2.Length;
            var newArr = new T[index];
            arr1.CopyTo(newArr, 0);
            arr2.CopyTo(newArr, arr1.Length);
            return newArr;
        }
        /// <summary>
        /// 截取数组元素,Span方式操作数组
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="arr">原数组</param>
        /// <param name="start">开始下标</param>
        /// <param name="lenght">截取长度</param>
        /// <returns></returns>
        public static T[] SliceSpan<T>(this T[] arr, int start, int lenght)
        {
            if (arr == null) return null;
            if (arr.Length < lenght - start) return arr;
            return arr.AsSpan().Slice(start, lenght).ToArray();
        }
        /// <summary>
        /// 截取数组元素,Span方式操作数组
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="arr">原数组</param>
        /// <param name="start">开始下标</param>
        /// <returns></returns>
        public static T[] SliceSpan<T>(this T[] arr, int start)
        {
            if (arr == null) return null;
            return arr.AsSpan().Slice(start).ToArray();
        }
        /// <summary>
        /// 裁剪数组元素,Span方式操作数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr">原数组</param>
        /// <param name="start">起始下标</param>
        /// <param name="end">结束下标</param>
        /// <returns></returns>
        public static T[] CutSpan<T>(this T[] arr, int start, int end)
        {
            if (arr == null) return null;
            if (end < start) return arr;
            if (start < 0) start = 0;
            if (end > arr.Length - 1) end = arr.Length - 1;
            return arr.AsSpan().Slice(start, end - start + 1).ToArray();
        }
        /// <summary>
        /// 合并两个数组,通过Linq.Concat()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr1">原数组</param>
        /// <param name="arr2">新增数组</param>
        /// <returns></returns>
        private static IEnumerable<T> AppendLinq<T>(this IEnumerable<T> arr1, T[] arr2)
        {
            return arr1.Concat(arr2);
        }
        /// <summary>
        /// 合并两个数组,通过CopyTo()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr1">原数组</param>
        /// <param name="arr2">新增数组</param>
        /// <returns></returns>
        private static T[] Append<T>(this T[] arr1, T[] arr2)
        {
            int index = arr1.Length + arr2.Length;
            var arr = new T[index];
            arr1.CopyTo(arr, 0);
            arr2.CopyTo(arr, arr1.Length);
            return arr;
        }
        /// <summary>
        /// 数组合并，通过Linq.Concat()，已做空处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">原数组</param>
        /// <param name="arrList">新增数组</param>
        /// <returns></returns>
        public static T[] AddByLq<T>(this T[] array, params T[][] arrList)
        {
            if (array == null) return array;
            if (arrList == null) return array;
            if (arrList.Length <= 0) return array;
            IEnumerable<T> result = array;
            foreach (var arr in arrList)
            {
                if (arr != null)
                {
                    result = result.AppendLinq(arr);
                }
            }
            return result.ToArray();
        }
        /// <summary>
        /// 数组合并 ,通过CopyTo(),已做空处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">原数组</param>
        /// <param name="arrList">新增数组</param>
        /// <returns></returns>
        public static T[] Add<T>(this T[] array, params T[][] arrList)
        {
            if (array == null) return array;
            if (arrList == null) return array;
            if (arrList.Length <= 0) return array;
            T[] result = array;
            foreach (var arr in arrList)
            {
                if (arr != null)
                {
                    result = result.Append(arr);
                }
            }
            return result;
        }
    }
    public static class ListExtensions
    {
        /// <summary>
        /// List转线程安全ConcurrentQueue队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static ConcurrentQueue<T> ToSaveQueue<T>(List<T> list)
        {
            ConcurrentQueue<T> queue = new ConcurrentQueue<T>();
            if (list != null)
            {
                list.ForEach(x =>
                {
                    queue.Enqueue(x);
                });
            }
            return queue;
        }
        /// <summary>
        /// List转线程安全ConcurrentBag,无序集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static ConcurrentBag<T> ToSaveBag<T>(List<T> list)
        {
            ConcurrentBag<T> bag = new ConcurrentBag<T>();
            if (list != null)
            {
                list.ForEach(x =>
                {
                    bag.Add(x);
                });
            }
            return bag;
        }
    }
}
