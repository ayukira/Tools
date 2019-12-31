using System;

namespace Tools
{
    public interface IJson
    {
        string ObjectToJson<T>(T obj);
        T JsonToModel<T>(string jsonStr);
        object JsonToObj(string jsonStr);
    }
}