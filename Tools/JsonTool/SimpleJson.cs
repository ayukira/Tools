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
        static IJson _json;
        static SimpleJson()
        {
            var inc = new Lazy<JsonHelper>(() => { return new JsonHelper(); }, true);
            if (inc.IsValueCreated)
            {
                _json = inc.Value;
            }
        }
        //序列化
        public static string ObjectToJson<T>(T obj)
        {
            return _json.ObjectToJson(obj);
        }
        // 反序列化
        public static T JsonToModel<T>(string jsonStr)
        {
            return _json.JsonToModel<T>(jsonStr);
        }
        // 反序列化
        public static object JsonToObj(string jsonStr)
        {
            return _json.JsonToObj(jsonStr);
        }
    }
}