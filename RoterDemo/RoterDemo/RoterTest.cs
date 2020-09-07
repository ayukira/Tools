using System;
using System.Collections.Generic;
using System.Text;

namespace RoterDemo
{
    public class RoterTest : IRouter
    {
        [MyRoter("0")]
        public string Print(string msg) 
        {
            Console.WriteLine($"Print0: {msg}");
            return msg;
        }
        [MyRoter("1")]
        public string Print1(string msg)
        {
            Console.WriteLine($"Print1: {msg}");
            return msg;
        }
        public string Print2(string msg)
        {
            Console.WriteLine($"Print2: {msg}");
            return msg;
        }
        [MyRoter("3")]
        public string Print3(string msg)
        {
            Console.WriteLine($"Print3: {msg}");
            return msg;
        }
    }
}
