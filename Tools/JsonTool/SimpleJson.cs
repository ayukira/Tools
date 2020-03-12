using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Tools
{
    //待完善，目前使用 Newtonsoft.Json;
    public sealed class SimpleJson
    {
        static Lazy<SimpleJson> inc = new Lazy<SimpleJson>(() => { return new SimpleJson(); }, true);
        static IJson _json;
        private SimpleJson()
        {
            _json = new JsonHelper();
        }
        public static SimpleJson Inc 
        {
            get 
            {
                return inc.Value;
            }
        }
        //序列化
        public string ObjectToJson<T>(T obj)
        {
            return _json.ObjectToJson(obj);
        }
        // 反序列化
        public T JsonToModel<T>(string jsonStr)
        {
            return _json.JsonToModel<T>(jsonStr);
        }
        // 反序列化
        public object JsonToObj(string jsonStr)
        {
            return _json.JsonToObj(jsonStr);
        }
    }
}