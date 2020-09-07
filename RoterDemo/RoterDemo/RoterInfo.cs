using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RoterDemo
{
    public class RoterInfo
    {
        public string ClassFullName { get; set; }
        public string MethodName { get; set; }
        public Type ClassType { get; set; }
        public MethodInfo methodInfo { get; set; }
    }
}
