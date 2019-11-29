using CSRedis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRedisTool
{
    public static class RedisExample
    {
        private static CSRedisClient redis = RedisHelper.Instance;

        public static DataExample CacheTest()
        {
            return HashCache("test", 300, () =>
               new DataExample { Id = 1, Name = "cyan", Time = DateTime.Now });
        }
        /// <summary>
        /// 加缓存壳
        /// </summary>
        public static void CacheShell()
        {
            //普通kv 
            var result1 = redis.CacheShell("key1", 10, () =>
                 {
                     return DateTime.Now;
                 });
            //hash数据
            var result2 = redis.CacheShell("key2", "time", 10, () =>
              {
                  return DateTime.Now;
              });
        }
        /// <summary>
        /// 普通Model Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelKey"></param>
        /// <param name="model"></param>
        public static void SetHash<T>(string modelKey, T model) where T : new()
        {
            foreach (var p in model.GetType().GetProperties())
            {
                redis.HSet(modelKey, p.Name, p.GetValue(model));
            }
        }
        /// <summary>
        /// 普通Model Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static T GetHash<T>(string modelKey) where T:new()
        {
            var dics = redis.HGetAll(modelKey);
            if (dics == null || dics.Count <= 0) 
            {
                return default;
            }
            T model = new T();
            foreach (var p in typeof(T).GetProperties()) 
            {
                if (p.CanWrite&&dics.TryGetValue(p.Name, out string value)) 
                {
                    p.SetValue(model, Convert.ChangeType(value, p.PropertyType));
                }
            }
            return model;
        }
        /// <summary>
        /// HashMap缓存删除
        /// </summary>
        /// <param name="key"></param>
        public static void HashMDel(string key) 
        {
            redis.Expire(key, -1);
        }
        /// <summary>
        /// Hash缓存Shell
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T HashCache<T>(string key, int timeoutSeconds, Func<T> func) where T : class ,new()
        {
            T ret = new T();
            Type type = typeof(T);
            var keys = GetKeys(type);
            var objs = redis.CacheShell(key, keys, timeoutSeconds, keys => Getdata(func()));
            if (objs != null)
            {
                var pros = type.GetProperties();
                foreach (var obj in objs) 
                {
                    foreach (var p in pros)
                    {
                        if (p.CanWrite&& p.Name == obj.key)
                        {
                            var name = obj.key;
                            var value = Convert.ChangeType(obj.value, p.PropertyType);
                            type.GetProperty(name).SetValue(ret, value);
                            break;
                        }
                    }
                }
            }
            else
            {
                ret = func();
            }
            return ret;
        }
        private static string[] GetKeys(Type type)
        {
            List<string> result = new List<string>();
            foreach (var property in type.GetProperties())
            {
                if(property.CanWrite)
                    result.Add(property.Name);
            }
            return result.ToArray();
        }
        private static (string, object)[] Getdata<T>(T model)
        {
            List<(string, object)> list = new List<(string, object)>();

            foreach (var p in model.GetType().GetProperties())
            {
                if (p.CanWrite)
                    list.Add((p.Name, p.GetValue(model)));
            }
            return list.ToArray();
        }
    }
    public class DataExample
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
}

