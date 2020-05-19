using System;
using System.Text.Json.Serialization;

namespace Tools
{
    /// <summary>
    /// Newtonsoft.Json
    /// </summary>
    class JsonHelper : IJson
    {
        Newtonsoft.Json.JsonSerializerSettings settings = null;
        public JsonHelper(Newtonsoft.Json.JsonSerializerSettings settings = null) 
        {
            if (settings == null)
            {
                settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(), //小写命名法。
                    DateFormatString = "yyyy-MM-dd HH:mm:ss.fff",//解决json时间带T的问题
                    Formatting = Newtonsoft.Json.Formatting.Indented,//解决json格式化缩进问题
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore//解决json序列化时的循环引用问题
                };
            }
            this.settings = settings;
        }
        public string ObjectToJson<T>(T obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, settings);
        }
        public T JsonToModel<T>(string jsonStr)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonStr, settings);
        }
        public object JsonToObj(string jsonStr)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonStr, settings);
        }
    }
    /// <summary>
    /// Text.Json
    /// </summary>
    class JsonHelper2 : IJson
    {
        static System.Text.Json.JsonSerializerOptions options;
        static JsonHelper2()
        {
            options = new System.Text.Json.JsonSerializerOptions();
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);
            options.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            options.Converters.Add(new DateTimeConverter());
            options.Converters.Add(new DateTimeNullableConverter());
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
    /// <summary>
    /// 自定义时间格式转换
    /// </summary>
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(System.Text.Json.Utf8JsonWriter writer, DateTime value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }
    }
    /// <summary>
    /// 自定义时间格式转换
    /// </summary>
    public class DateTimeNullableConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            return string.IsNullOrEmpty(reader.GetString()) ? default(DateTime?) : DateTime.Parse(reader.GetString());
        }

        public override void Write(System.Text.Json.Utf8JsonWriter writer, DateTime? value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }
    }
}