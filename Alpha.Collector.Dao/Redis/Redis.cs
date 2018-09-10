using StackExchange.Redis;
using System;
using System.Configuration;

namespace Alpha.Collector.Dao
{
    /// <summary>
    /// Redis操作类
    /// </summary>
    public static partial class RedisHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static readonly string ConnectString = ConfigurationManager.ConnectionStrings["RedisConnection"].ToString();

        /// <summary>
        /// 延迟加载
        /// </summary>
        private static Lazy<ConnectionMultiplexer> _lazyConnection;

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly Object MultiplexerLock = new Object();

        /// <summary>
        /// 实例
        /// </summary>
        private static readonly IDatabase Cache;

        /// <summary>
        /// 构造函数
        /// </summary>
        static RedisHelper()
        {
            var conn = CreateManager.Value;
            Cache = conn.GetDatabase(); 
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static Lazy<ConnectionMultiplexer> GetManager(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = GetDefaultConnectionString();
            }

            return new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString));
        }

        /// <summary>
        /// 创建连接
        /// </summary>
        private static Lazy<ConnectionMultiplexer> CreateManager
        {
            get
            {
                if (_lazyConnection == null)
                {
                    lock (MultiplexerLock)
                    {
                        if (_lazyConnection != null) return _lazyConnection;

                        _lazyConnection = GetManager();
                        return _lazyConnection;
                    }
                }

                return _lazyConnection;
            }
        }

        /// <summary>
        /// 获取默认连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultConnectionString()
        {
            return ConnectString;
        }
    }
}
