using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
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
            if (arr2.Length <= 0) return arr1;
            int index = arr1.Length + arr2.Length;
            var arr = new T[index];
            arr1.CopyTo(arr, 0);
            arr2.CopyTo(arr, arr1.Length);
            return arr;
        }
        /// <summary>
        /// 数组合并,通过Linq.Concat(),数组较小,较少时请使用Add()方法,已做空处理
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
        /// 数组合并 ,通过CopyTo(),数组较大时请使用AddByLq()方法,已做空处理
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
}
