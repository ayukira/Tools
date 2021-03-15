namespace Tools.Json
{
    /// <summary>
    /// Text.Json
    /// </summary>
    public static class JsonTool
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson<T>(T obj) 
        {
            return SimpleJson.Inc.ObjectToJson(obj);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObject<T>(string json)
        {
            return SimpleJson.Inc.JsonToModel<T>(json);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object ToObject(string json)
        {
            return SimpleJson.Inc.JsonToObj(json);
        }
    }
    /// <summary>
    /// Newtonsoft.Json
    /// </summary>
    public static class JosoUtil
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(T obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T JsonToModel<T>(string jsonStr)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonStr);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static object JsonToObj(string jsonStr)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonStr);
        }
    }
}
