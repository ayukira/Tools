using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Tools
{
    public sealed class MD5Encrypt
    {
        /// <summary>
        /// 签名字符串
        /// </summary>
        /// <param name="dataStr">需要签名的字符串</param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>签名结果</returns>
        public static string Sign(string dataStr, string encryptKey, Encoding encoding)
        {
            StringBuilder sb = new StringBuilder(32);

            dataStr += encryptKey;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(encoding.GetBytes(dataStr));
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="dataStr">需要签名的字符串</param>
        /// <param name="sign">签名结果</param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>验证结果</returns>
        public static bool Verify(string dataStr, string sign, string encryptKey, Encoding encoding)
        {
            string mysign = Sign(dataStr, encryptKey, encoding);
            if (mysign == sign)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 非标准32位MD5
        /// </summary>
        /// <param name="strEnc"></param>
        /// <returns></returns>
        public static string MD5EncryptSpecial(string strEnc)
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
        public static string MD5Encrypt32(string strEnc)
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
    }
}
