using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public static class ListExtensions
    {
        /// <summary>
        /// 移除符合条件的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static IEnumerable<T> Remove<T>(this IEnumerable<T> list, Func<T, bool> match)
        {
            if (list == null) return null;
            return list.Except(list.Where(match));
        }
        /// <summary>
        /// List转线程安全ConcurrentQueue队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static ConcurrentQueue<T> ToSaveQueue<T>(this IEnumerable<T> list)
        {
            ConcurrentQueue<T> queue = new ConcurrentQueue<T>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    queue.Enqueue(item);
                }
            }
            return queue;
        }
        /// <summary>
        /// List转线程安全ConcurrentBag,无序集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static ConcurrentBag<T> ToSaveBag<T>(this IEnumerable<T> list)
        {
            ConcurrentBag<T> bag = new ConcurrentBag<T>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    bag.Add(item);
                }
            }
            return bag;
        }
    }
}
