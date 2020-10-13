using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Tools
{
    public static class CommonTools
    {
        /// <summary>
        /// Guid.NewGuid().GetHashCode() Seed
        /// </summary>
        /// <returns></returns>
        public static Random GetRandom()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r;
        }

        /// <summary>
        /// 通用泛型转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="input">输入值</param>
        /// <param name="defau">默认值</param>
        public static T Parse<T>(object input, T defau = default)
        {
            if (input == null)
                return defau;

            return (T)Parse(input, typeof(T), defau);
        }
        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="input"></param>
        /// <param name="type"></param>
        /// <param name="defau"></param>
        /// <returns></returns>
        public static object Parse(object input, Type type, object defau)
        {
            if (input == null)
                return defau;

            var typeName = type.Name.ToLower();
            try
            {
                if (typeName == "string")
                    return input.ToString();

                if (typeName == "guid")
                    return new Guid(input.ToString());

                if (type.IsEnum)
                    return Enum.Parse(type, input.ToSafeString(), true);

                if (input is IConvertible)
                    return Convert.ChangeType(input, type);

                return input;
            }
            catch
            {
                return defau;
            }
        }

        #region String 处理

        #endregion

        #region List
        /// <summary>
        /// 删除数组中的重复项
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T[] RemoveDup<T>(T[] values)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < values.Length; i++) //遍历数组成员
            {
                if (!list.Contains(values[i]))
                {
                    list.Add(values[i]);
                };
            }
            return list.ToArray();
        }
        #endregion

        #region Exchange
        /// <summary>
        /// String to Int32
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defvalue"></param>
        /// <returns></returns>
        public static int ToInt(string value, int defvalue = 0)
        {
            var _ = int.TryParse(value, out int result);
            if (!_) { result = defvalue; }
            return result;
        }
        /// <summary>
        /// String to Int64
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defvalue"></param>
        /// <returns></returns>
        public static long ToInt64(string value, long defvalue = 0)
        {
            var _ = long.TryParse(value, out long result);
            if (!_) { result = defvalue; }
            return result;
        }
        /// <summary>
        /// String to DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string value)
        {
            return ToDateTime(value, default);
        }
        /// <summary>
        /// String to DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defvalue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string value, DateTime defvalue)
        {
            var _ = DateTime.TryParse(value, out DateTime result);
            if (!_) { result = defvalue; }
            return result;
        }
        /// <summary>
        /// String to Decimal
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defvalue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(string value, decimal defvalue = 0m)
        {
            var _ = decimal.TryParse(value, out decimal result);
            if(!_) { result = defvalue; }
            return result;
        }
        #endregion
    }
}