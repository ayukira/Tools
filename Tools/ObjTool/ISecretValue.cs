using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
    /// <summary>
    /// 内存加密 float
    /// </summary>
    public struct SecretFloat : ISecretValue<float>
    {
        private char[] _SecretValue;
        private int xor;
        /// <summary>
        /// Value float
        /// </summary>
        public float Value { get { return SecretGet(_SecretValue); } set { _SecretValue = SecretSet(value); } }
        private float SecretGet(char[] secretValue)
        {
            return float.TryParse(Xor(secretValue, xor), out float result) ? result : 0;
        }
        private char[] SecretSet(float originalValue)
        {
            return Xor(originalValue.ToString(), RandomXor());
        }
        private int RandomXor()
        {
            xor = new Random(Guid.NewGuid().GetHashCode()).Next(0x000000, 0x000fff);
            return xor;
        }
        private char[] Xor(string input, int xor)
        {
            char[] a = input.ToCharArray();
            char[] b = new char[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                char c = (char)(a[i] ^ xor);
                b[i] = c;
            }
            return b;
        }
        private string Xor(char[] input, int xor)
        {
            string ouput;
            char[] a = input;
            char[] b = new char[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                char c = (char)(a[i] ^ xor);
                b[i] = c;
            }
            ouput = new string(b);
            return ouput;
        }
    }
    /// <summary>
    /// 内存加密 double
    /// </summary>
    public struct SecretDouble : ISecretValue<double>
    {
        private char[] _SecretValue;
        private int xor;
        /// <summary>
        /// Value double
        /// </summary>
        public double Value { get { return SecretGet(_SecretValue); } set { _SecretValue = SecretSet(value); } }
        private double SecretGet(char[] secretValue)
        {
            return double.TryParse(Xor(secretValue, xor), out double result) ? result : 0;
        }
        private char[] SecretSet(double originalValue)
        {
            return Xor(originalValue.ToString(), RandomXor());
        }
        private int RandomXor()
        {
            xor = new Random(Guid.NewGuid().GetHashCode()).Next(0x000000, 0x000fff);
            return xor;
        }
        private char[] Xor(string input, int xor)
        {
            char[] a = input.ToCharArray();
            char[] b = new char[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                char c = (char)(a[i] ^ xor);
                b[i] = c;
            }
            return b;
        }
        private string Xor(char[] input, int xor)
        {
            string ouput;
            char[] a = input;
            char[] b = new char[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                char c = (char)(a[i] ^ xor);
                b[i] = c;
            }
            ouput = new string(b);
            return ouput;
        }
    }
    /// <summary>
    /// 内存加密 int
    /// </summary>
    public struct SecretInt : ISecretValue<int>
    {
        private int _SecretValue;
        private int xor;
        /// <summary>
        /// Value int
        /// </summary>
        public int Value { get { return SecretGet(_SecretValue); } set { _SecretValue = SecretSet(value); } }
        private int SecretGet(int secretValue)
        {
            return secretValue ^ xor;
        }
        private int SecretSet(int originalValue)
        {
            return originalValue ^ RandomXor();
        }
        private int RandomXor()
        {
            xor = new Random(Guid.NewGuid().GetHashCode()).Next(0x000000, 0x000fff);
            return xor;
        }
    }
    /// <summary>
    /// 内存加密 long
    /// </summary>
    public struct SecretLong : ISecretValue<long>
    {
        private long _SecretValue;
        private int xor;
        /// <summary>
        /// Value long
        /// </summary>
        public long Value { get { return SecretGet(_SecretValue); } set { _SecretValue = SecretSet(value); } }
        private long SecretGet(long secretValue)
        {
            return secretValue ^ xor;
        }
        private long SecretSet(long originalValue)
        {
            return originalValue ^ RandomXor();
        }
        private int RandomXor()
        {
            xor = new Random(Guid.NewGuid().GetHashCode()).Next(0x000000, 0x000fff);
            return xor;
        }
    }
    /// <summary>
    /// 内存加密 string
    /// </summary>
    public struct SecretString : ISecretValue<string>
    {
        private char[] _SecretValue;
        private int xor;
        /// <summary>
        /// Value String
        /// </summary>
        public string Value { get { return SecretGet(_SecretValue); } set { _SecretValue = SecretSet(value); } }
        private string SecretGet(char[] secretValue)
        {
            return Xor(secretValue, xor);
        }
        private char[] SecretSet(string originalValue)
        {
            return Xor(originalValue, RandomXor());
        }
        private int RandomXor()
        {
            xor = new Random(Guid.NewGuid().GetHashCode()).Next(0x000000, 0x000fff);
            return xor;
        }
        private char[] Xor(string input, int xor)
        {
            var a = input.ToCharArray();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = (char)(a[i] ^ xor);
            }
            return a.ToArray();
        }
        private string Xor(char[] input, int xor)
        {
            string ouput;
            var a = input;
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = (char)(a[i] ^ xor);
            }
            ouput = new string(a);
            return ouput;
        }
    }
    /// <summary>
    /// ISecretValue
    /// </summary>
    /// <typeparam name="T">Struct</typeparam>
    interface ISecretValue<T>
    {
        /// <summary>
        /// Value
        /// </summary>
        T Value { get; set; }
    }
}
