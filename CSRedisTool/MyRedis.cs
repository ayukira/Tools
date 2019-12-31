/*
 * Author Github:https://github.com/2881099/csredis
 */
using System;
using System.Collections.Generic;
using System.Text;
using CSRedis;

namespace CSRedisTool
{
    #region 多个Redis实例
    public abstract class MyRedis1 : RedisHelper<MyRedis1> { }
    public abstract class MyRedis2 : RedisHelper<MyRedis2> { }
    #endregion

    public class MyRedis
    {
        public const string Str_key = "k_";
        public const string Hash_key = "h_";
        public const string Set_key = "s_";
        public const string SortSet_key = "z_";
        public const string List_key = "l_";
        public static void Init()
        {
            // CSRedis 应用层分区  null 默认分区方案
            var csredis = new CSRedisClient(null, "127.0.0.1:6379,defaultDatabase=1,prefix=proj1_", "127.0.0.1:6379,defaultDatabase=2,prefix=proj1_");
            //注册默认静态类RedisHelper
            RedisHelper.Initialization(csredis);

            //操作多个库
            MyRedis1.Initialization(new CSRedisClient("127.0.0.1:6379,defaultDatabase=3,prefix=proj1_"));
            MyRedis2.Initialization(new CSRedisClient("127.0.0.1:6379,defaultDatabase=4,prefix=proj1_"));
        }
    }
    public static class CSRedisExtensions
    {

    }
}