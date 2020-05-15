using System;
using System.Runtime.Caching;

namespace Tools
{
    /// <summary>
    /// 单机简单缓存工具,不适合缓存大量数据,大量数据请使用Redis
    /// </summary>
    public class SimpleCache : CacheBase
    {
        private const string CacheString = "SimpleCache";
        private MemoryCache _memoryCache = new MemoryCache(CacheString);
        private static Lazy<SimpleCache> _instance = new Lazy<SimpleCache>(() => { return new SimpleCache(); }, true);
        private SimpleCache() : base() { }
        /// <summary>
        /// 获取缓存实例
        /// </summary>
        public static SimpleCache Instance { get { return _instance.Value; } }
        /// <summary>
        /// 检查缓存实例是否创建
        /// </summary>
        public static bool IsCreate { get { return _instance.IsValueCreated; } }
        /// <summary>
        /// 获取指定缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override object GetOrDefault(string key)
        {
            return _memoryCache.Get(key);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Obj</param>
        /// <param name="slidingExpireTime">多久未访问则失效</param>
        /// <param name="absoluteExpireTime">超时失效</param>
        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (value == null)
            {
                throw new Exception("Can not insert null values to the cache!");
            }
            var cachePolicy = new CacheItemPolicy();
            if (absoluteExpireTime != null)
            {
                cachePolicy.AbsoluteExpiration = DateTimeOffset.Now.Add(absoluteExpireTime.Value);
            }
            else if (slidingExpireTime != null)
            {
                cachePolicy.SlidingExpiration = slidingExpireTime.Value;
            }
            else
            {
                cachePolicy.AbsoluteExpiration = DateTimeOffset.Now.Add(TimeSpan.FromSeconds(60));
            }
            _memoryCache.Set(key, value, cachePolicy);
        }
        /// <summary>
        /// 设置缓存,滑动失效,多久未访问失效
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Obj</param>
        /// <param name="slidingExpireTime">多久未访问则失效</param>
        public void SetBySlid(string key, object value, TimeSpan slidingExpireTime)
        {
            if (value == null)
            {
                throw new Exception("Can not insert null values to the cache!");
            }
            var cachePolicy = new CacheItemPolicy();
            cachePolicy.SlidingExpiration = slidingExpireTime;
            _memoryCache.Set(key, value, cachePolicy);
        }
        /// <summary>
        /// 设置缓存,设置失效时间
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Obj</param>
        /// <param name="absoluteExpireTime">失效时间</param>
        public void Set(string key, object value, DateTime dateTime)
        {
            if (value == null)
            {
                return;
                throw new Exception("Can not insert null values to the cache!");
            }
            var now = DateTime.Now;
            if (now > dateTime)
            {
                return;
            }
            TimeSpan ts = dateTime.Subtract(now);
            Set(key, value, ts);
        }
        /// <summary>
        /// 设置缓存,缓存时间：单位毫秒
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">obj</param>
        /// <param name="ExprieTime">失效时间,毫秒时间戳</param>
        public override void Set(string key, object value, long ExprieTime)
        {
            if (value == null)
            {
                return;
                throw new Exception("Can not insert null values to the cache!");
            }
            var cachePolicy = new CacheItemPolicy();
            cachePolicy.AbsoluteExpiration = DateTimeOffset.Now.Add(TimeSpan.FromMilliseconds(ExprieTime));
            _memoryCache.Set(key, value, cachePolicy);
        }
        /// <summary>
        /// 设置缓存,缓存时间：TimeSpan
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Obj</param>
        /// <param name="ExprieTime">失效时间 TimeSpan</param>
        public override void Set(string key, object value, TimeSpan ExprieTime)
        {
            if (value == null)
            {
                return;
                throw new Exception("Can not insert null values to the cache!");
            }
            var cachePolicy = new CacheItemPolicy();
            TimeSpan ts = ExprieTime;
            cachePolicy.AbsoluteExpiration = DateTimeOffset.Now.Add(ts);
            _memoryCache.Set(key, value, cachePolicy);
        }
        /// <summary>
        /// 检查指定缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool IsExists(string key)
        {
            return !(Get(key) is null);
        }
        /// <summary>
        /// 移除指定缓存
        /// </summary>
        /// <param name="key"></param>
        public override void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
        /// <summary>
        /// 清除整个缓存
        /// </summary>
        public override void Clear()
        {
            // 将原来的释放，并新建一个cache
            _memoryCache.Dispose();
            _memoryCache = new MemoryCache(CacheString);
        }
        /// <summary>
        /// 回收整个缓存
        /// </summary>
        public override void Dispose()
        {
            _memoryCache.Dispose();
            _memoryCache = new MemoryCache(CacheString);
        }
    }
}