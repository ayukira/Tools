using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Tools
{
    public class JsonHelper
    {
        public static string ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        // 反序列化
        public static T JsonToModel<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
        public static object JsonToObj(string jsonStr)
        {
            return JsonConvert.DeserializeObject(jsonStr);
        }
    }
}
