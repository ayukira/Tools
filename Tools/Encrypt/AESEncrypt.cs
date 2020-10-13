using System;
using System.Security.Cryptography;
using System.Text;

namespace Tools
{
    public sealed class AESEncrypt
    {

        /// <summary>
        /// 128位0向量
        /// </summary>
        private static readonly byte[] AES_IV = InitIv(16);

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="encryptKey"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Encrypt(string dataStr, string encryptKey, Encoding encoding)
        {
            return Encrypt(dataStr, encryptKey, encoding, AES_IV);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="encryptKey"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Encrypt(string dataStr, string encryptKey, Encoding encoding, byte[] iv)
        {

            Byte[] keyArray = Convert.FromBase64String(encryptKey);
            Byte[] toEncryptArray = encoding.GetBytes(dataStr);
            RijndaelManaged rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                IV = iv
            };

            ICryptoTransform cTransform = rDel.CreateEncryptor(rDel.Key, rDel.IV);
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="encryptKey"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Decrypt(string dataStr, string encryptKey, Encoding encoding)
        {

            Console.WriteLine(System.Text.Encoding.Default.GetString(AES_IV));
            return Decrypt(dataStr, encryptKey, encoding, AES_IV);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="encryptKey"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Decrypt(string dataStr, string encryptKey, Encoding encoding, byte[] iv)
        {
            Byte[] keyArray = Convert.FromBase64String(encryptKey);
            Byte[] toEncryptArray = Convert.FromBase64String(dataStr);

            RijndaelManaged rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = System.Security.Cryptography.CipherMode.CBC,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7,
                IV = iv
            };

            ICryptoTransform cTransform = rDel.CreateDecryptor(rDel.Key, rDel.IV);
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return encoding.GetString(resultArray);
        }

        /// <summary>
        /// 初始化向量
        /// </summary>
        /// <param name="blockSize"></param>
        /// <returns></returns>
        private static byte[] InitIv(int blockSize)
        {
            byte[] iv = new byte[blockSize];
            for (int i = 0; i < blockSize; i++)
            {
                iv[i] = (byte)0x0;
            }
            return iv;

        }

        /// <summary>
        /// 随机生成32位AESkey
        /// </summary>
        /// <returns></returns>
        public static string GenerateAESKey()
        {
            string str = string.Empty;

            Random rnd1 = new Random();
            int r = rnd1.Next(10, 100);

            long num2 = DateTime.Now.Ticks + r;

            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> r)));
            for (int i = 0; i < 32; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str += ch.ToString();
            }
            return str;
        }

    }
}
