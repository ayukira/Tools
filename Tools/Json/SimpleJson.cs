using System;

namespace Tools
{
    /// <summary>
    /// 待完善，目前使用 Text.Json
    /// </summary>
    internal sealed class SimpleJson
    {
        static Lazy<SimpleJson> inc = new Lazy<SimpleJson>(() => { return new SimpleJson(); }, true);
        static IJson _json;
        private SimpleJson()
        {
            _json = new JsonHelper2();
        }
        public static SimpleJson Inc 
        {
            get 
            {
                return inc.Value;
            }
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string ObjectToJson<T>(T obj)
        {
            return _json.ObjectToJson(obj);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public T JsonToModel<T>(string jsonStr)
        {
            return _json.JsonToModel<T>(jsonStr);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public object JsonToObj(string jsonStr)
        {
            return _json.JsonToObj(jsonStr);
        }
    }
}