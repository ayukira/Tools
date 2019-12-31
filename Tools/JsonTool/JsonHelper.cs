using System;
using System.Collections.Generic;
using System.Text;

namespace Tools
{
    /// <summary>
    /// Newtonsoft.Json
    /// </summary>
    public class JsonHelper : IJson
    {
        public string ObjectToJson<T>(T obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        public T JsonToModel<T>(string jsonStr)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonStr);
        }
        public object JsonToObj(string jsonStr)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonStr);
        }
    }
    /// <summary>
    /// Text.Json
    /// </summary>
    public class JsonHelper2 : IJson
    {
        static System.Text.Json.JsonSerializerOptions options;
        static JsonHelper2()
        {
            options = new System.Text.Json.JsonSerializerOptions();
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);
        }
        public string ObjectToJson<T>(T obj)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(obj, options);
            return json;
        }
        public T JsonToModel<T>(string jsonStr)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(jsonStr, options);
        }
        public object JsonToObj(string jsonStr)
        {
            return System.Text.Json.JsonSerializer.Deserialize<object>(jsonStr);
        }
    }
}