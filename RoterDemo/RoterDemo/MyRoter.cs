using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Reflection;
using System.Linq;

namespace RoterDemo
{
    public class MyRoter : Attribute
    {
        private static bool Status = false;
        private static ConcurrentDictionary<string, object> ClassInstance = new ConcurrentDictionary<string, object>();
        private static ConcurrentDictionary<string, RoterInfo> RoterInfo = new ConcurrentDictionary<string, RoterInfo>();

        public string Route { get; private set; }

        public MyRoter(string route)
        {
            Route = route;
        }

        public static object GetInstance(string fullName)
        {
            if (!ExistsInstance(fullName))
                return null;
            if (!ClassInstance.TryGetValue(fullName, out object ins))
                return null;
            return ins;
        }
        public static bool AddInstance(string fullName, object ins)
        {
            if (ExistsInstance(fullName)) return true;
            return ClassInstance.TryAdd(fullName, ins);
        }
        public static bool ExistsInstance(string fullName)
        {
            return ClassInstance.ContainsKey(fullName);
        }
        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
        public static bool ExistsRoute(string route)
        {
            return RoterInfo.ContainsKey(route);
        }
        public static RoterInfo GetRouterInfo(string route) 
        {
            RoterInfo roterInfo= null;
            RoterInfo.TryGetValue(route, out roterInfo);
            return roterInfo;
        }
        public static void BuildRoter()
        {
            if (Status) { return; }
            Status = true;
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (!typeof(IRouter).IsAssignableFrom(type)) { continue; }
                foreach (var method in type.GetMethods())
                {
                    var attr = method.GetCustomAttributes().OfType<MyRoter>().FirstOrDefault();
                    if (attr == null) { continue; }
                    if (ExistsRoute(attr.Route)) { throw new Exception($"Rotue:[{attr.Route}] exists,check your route"); }
                    if (!ExistsInstance(type.FullName)) 
                    {
                        object ins = CreateInstance(type);
                        AddInstance(type.FullName, ins);
                    }
                    var roterInfo = new RoterInfo
                    {
                        MethodName = method.Name,
                        ClassType = type,
                        ClassFullName = type.FullName,
                        methodInfo = method,
                    };
                    if (!RoterInfo.TryAdd(attr.Route, roterInfo)) { Console.WriteLine($"Route :{attr.Route} Add Error"); continue; }
                    Console.WriteLine($"Route :{attr.Route} Add Success");
                }
            }
        }
    }
}
