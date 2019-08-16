using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Tools
{
    //待完善，目前使用 Newtonsoft.Json;
    public static class SimpleJson
    {
        //序列化
        public static string ObjectToJson(object obj)
        {
            return JsonHelper.ObjectToJson(obj);
        }
        // 反序列化
        public static T JsonToModel<T>(string jsonStr)
        {
            return JsonHelper.JsonToModel<T>(jsonStr);
        }
        // 反序列化
        public static object JsonToObj(string jsonStr)
        {
            return JsonHelper.JsonToObj(jsonStr);
        }
    }
}
