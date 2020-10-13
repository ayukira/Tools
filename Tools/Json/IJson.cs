namespace Tools
{
    /// <summary>
    /// IJson
    /// </summary>
    public interface IJson
    {
        /// <summary>
        /// ObjectToJson
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        string ObjectToJson<T>(T obj);
        /// <summary>
        /// JsonToModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        T JsonToModel<T>(string jsonStr);
        /// <summary>
        /// JsonToObject
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        object JsonToObj(string jsonStr);
    }
}