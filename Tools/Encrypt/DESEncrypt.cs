using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Tools
{
    public class DESEncrypt
    {
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="strText">明文</param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static string DesEncrypt(string strText, string encryptKey)
        {
            string outString = "";
            byte[] byKey = null;
            byte[] IV = Encoding.Default.GetBytes(encryptKey);
            try
            {
                byKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, encryptKey.Length));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Mode = CipherMode.ECB;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                outString = Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception)
            {
                outString = "";
            }
            return outString;
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="strText">密文</param>
        /// <param name="decryptKey">密钥</param>
        /// <returns></returns>
        public static string DesDecrypt(string strText, string decryptKey)
        {
            string outString = "";
            byte[] byKey = null;
            byte[] IV = Encoding.Default.GetBytes(decryptKey);
            byte[] inputByteArray = new Byte[strText.Length];
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(decryptKey.Substring(0, decryptKey.Length));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Mode = CipherMode.ECB;
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = new System.Text.UTF8Encoding();
                outString = encoding.GetString(ms.ToArray());
            }
            catch (Exception)
            {
                outString = "";
            }
            return outString;
        }
    }
}
