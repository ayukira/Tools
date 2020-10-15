using System;
using System.Collections.Generic;
using System.Text;

namespace Tools
{
    public class Base16Encrypt
    {
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
    }
    public class Base64Encrypt
    {
        public static string Encode(string input)
        {
            return Encode(input, Encoding.UTF8);
        }
        public static string Decode(string input)
        {
            return Decode(input, Encoding.UTF8);
        }

        public static string Encode(string input, Encoding encodeType)
        {
            string encode = string.Empty;
            byte[] bytes = encodeType.GetBytes(input);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = input;
            }
            return encode;
        }
        public static string Decode(string input, Encoding encodeType)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(input);
            try
            {
                decode = encodeType.GetString(bytes);
            }
            catch
            {
                decode = input;
            }
            return decode;
        }
    }
}
