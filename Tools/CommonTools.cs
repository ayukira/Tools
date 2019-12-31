using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Tools
{
    public static class CommonTools
    {
        public static Random GetRandom()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r;
        }

        #region String 处理
        /// <summary>
        /// 自定义Base16编码
        /// </summary>
        /// <param name="str">需要编码的字符串</param>
        /// <param name="autoCode">自定义Base16编码数组,16个元素,可以为数字、字符、特殊符号,若不填,使用默认的Base16编码数组,解码与编码的Base16编码数组一样</param>
        /// <returns></returns>
        public static string AutoBase16Encrypt(string str, string[] autoCode = null)
        {
            StringBuilder strEn = new StringBuilder();
            if (autoCode == null || autoCode.Length < 16)
                autoCode = new string[] { "a", "2", "B", "g", "E", "5", "f", "6", "C", "8", "o", "9", "Z", "p", "k", "M" };
            System.Collections.ArrayList arr = new System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes(str));
            for (int i = 0; i < arr.Count; i++)
            {
                byte data = (byte)arr[i];
                int v1 = data >> 4;
                strEn.Append(autoCode[v1]);
                int v2 = ((data & 0x0f) << 4) >> 4;
                strEn.Append(autoCode[v2]);
            }
            return strEn.ToString();
        }
        /// <summary>
        /// 自定义Base16解码
        /// </summary>
        /// <param name="str">需要解码的字符串</param>
        /// <param name="autoCode">自定义Base16编码数组,16个元素,可以为数字、字符、特殊符号,若不填,使用默认的Base16编码数组,解码与编码的Base16编码数组一样</param>
        /// <returns></returns>
        public static string AutoBase16Decrypt(string str, string[] autoCode = null)
        {
            int k = 0;
            string dnStr;
            int strLength = str.Length;
            if (autoCode == null || autoCode.Length < 16)
                autoCode = new string[] { "a", "2", "B", "g", "E", "5", "f", "6", "C", "8", "o", "9", "Z", "p", "k", "M" };
            byte[] data = new byte[strLength / 2];
            for (int i = 0, j = 0; i < data.Length; i++, j++)
            {
                byte s = 0;
                int index1 = autoCode.ToList().IndexOf(str[j].ToString());
                j += 1;
                int index2 = autoCode.ToList().IndexOf(str[j].ToString());
                s = (byte)(s ^ index1);
                s = (byte)(s << 4);
                s = (byte)(s ^ index2);
                data[k] = s;
                k++;
            }
            dnStr = Encoding.Default.GetString(data);
            return dnStr;
        }
        /// <summary>
        /// 非标准32位MD5
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string strEnc)
        {
            string cl = strEnc;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd += s[i].ToString("X");
            }
            md5.Dispose();
            return pwd.ToLower();
        }
        /// <summary>
        /// 标准32位MD5
        /// </summary>
        /// <param name="strEnc"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string strEnc)
        {
            try
            {
                // Create a new instance of the MD5CryptoServiceProvider object.
                MD5 md5Hasher = MD5.Create();
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(strEnc));
                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                md5Hasher.Dispose();
                // Return the hexadecimal string.
                return sBuilder.ToString().ToLower();
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 字符串简单异或运算,可逆
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        public static string Xor(string input)
        {
            string ouput;
            char[] a = input.ToCharArray();
            char[] b = new char[a.Count()];
            for (int i = 0; i < a.Count(); i++)
            {
                char c = (char)(a[i] ^ 10);
                b[i] = c;
            }
            ouput = new string(b);
            return ouput;
        }
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
        public static int ToInt(string value, int defvalue = 0)
        {
            var _ = int.TryParse(value, out int result);
            if (!_)
            {
                result = defvalue;
            }
            return result;
        }
        public static long ToInt64(string value, long defvalue = 0)
        {
            var _ = long.TryParse(value, out long result);
            if (!_)
            {
                result = defvalue;
            }
            return result;
        }
        public static DateTime ToDateTime(string value)
        {
            return ToDateTime(value, DateTime.MinValue);
        }
        public static DateTime ToDateTime(string value, DateTime defvalue)
        {
            var _ = DateTime.TryParse(value, out DateTime result);
            if (!_) { result = defvalue; }
            return result;
        }
        public static decimal ToDecimal(string value)
        {
            return ToDecimal(value, decimal.Zero);
        }
        public static decimal ToDecimal(string value, decimal defvalue)
        {
            if (decimal.TryParse(value, out decimal result))
            {
                return result;
            }
            return defvalue;
        }
        #endregion
    }
}