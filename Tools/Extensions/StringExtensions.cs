using System;
using System.Collections.Generic;
using System.Text;

namespace Tools
{
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
        /// <summary>
        /// string.IsNullOrEmpty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        /// <summary>
        /// string.IsNullOrWhiteSpace
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
