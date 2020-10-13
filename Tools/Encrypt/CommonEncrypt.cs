using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
    public class CommonEncrypt
    {
        /// <summary>
        /// 字符串简单异或运算,可逆
        /// </summary>
        /// <param name="input"></param>
        /// <param name="xorCode"></param>
        /// <returns></returns>
        public static string Xor(string input,uint xorCode = 10 )
        {
            string ouput;
            char[] a = input.ToCharArray();
            char[] b = new char[a.Count()];
            for (int i = 0; i < a.Count(); i++)
            {
                char c = (char)(a[i] ^ xorCode);
                b[i] = c;
            }
            ouput = new string(b);
            return ouput;
        }
    }
}
