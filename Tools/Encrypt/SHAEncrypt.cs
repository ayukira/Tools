using System.Security.Cryptography;
using System.Text;

namespace Tools
{
    public sealed class SHAEncrypt
    {
        /// <summary>
        /// SHA1 加密
        /// </summary>
        public static string Sha1(string dataStr, Encoding encoding)
        {
            if (string.IsNullOrEmpty(dataStr))
                return string.Empty;

            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] hashStr = sha1.ComputeHash(encoding.GetBytes(dataStr));
            StringBuilder sb = new StringBuilder();
            foreach (byte btStr in hashStr)
            {
                sb.AppendFormat("{0:X2}", btStr);
            }
            sha1.Dispose();
            return sb.ToString();
        }

        /// <summary>
        /// SHA256 加密
        /// </summary>
        public static string Sha256(string dataStr, Encoding encoding)
        {
            if (string.IsNullOrEmpty(dataStr))
                return string.Empty;

            SHA256 sha256 = new SHA256Managed();
            byte[] hashStr = sha256.ComputeHash(encoding.GetBytes(dataStr));
            StringBuilder sb = new StringBuilder();
            foreach (byte btStr in hashStr)
            {
                sb.AppendFormat("{0:X2}", btStr);
            }
            sha256.Dispose();
            return sb.ToString();
        }

        /// <summary>
        /// SHA512 加密
        /// </summary>
        public static string Sha512(string dataStr, Encoding encoding)
        {
            if (string.IsNullOrEmpty(dataStr))
                return string.Empty;

            SHA512 sha512 = new SHA512CryptoServiceProvider();

            byte[] hashStr = sha512.ComputeHash(encoding.GetBytes(dataStr));
            StringBuilder sb = new StringBuilder();
            foreach (byte btStr in hashStr)
            {
                sb.AppendFormat("{0:X2}", btStr);
            }
            sha512.Dispose();
            return sb.ToString();
        }
    }
}
