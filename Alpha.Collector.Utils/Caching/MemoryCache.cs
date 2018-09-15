using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace Alpha.Collector.Utils
{
    /// <summary>
    /// Represents a MemoryCacheCache
    /// </summary>
    public partial class MemoryCache : ICache
    {
        public MemoryCache() { }

        protected ObjectCache Cache
        {
            get
            {
                return System.Runtime.Caching.MemoryCache.Default;
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public T Get<T>(string key)
        {
            if (Cache.Contains(key))
            {
                return (T)Cache[key];
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns></returns>
        public object Get(string key)
        {
            return Cache[key];
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time(unit:minute) 默认30秒</param>
        public void Add(string key, object data, long cacheTime = 30)
        {
            if (data == null)
            {
                return;
            }

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromSeconds(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public bool Contains(string key)
        {
            return Cache.Contains(key);
        }

        /// <summary>
        /// 或者缓存项总数
        /// </summary>
        public int Count
        {
            get { return (int)(Cache.GetCount()); }
        }

        /// <summary>
        /// 所有键
        /// </summary>
        public string GetAllKeys
        {
            get
            {
                List<string> keyList = new List<string>();
                foreach (var item in Cache)
                {
                    keyList.Add(item.Key);
                }
                keyList.Sort();
                return string.Join(", ", keyList.ToArray());
            }
        }


        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var keysToRemove = new List<String>();
            foreach (var item in Cache)
            {
                if (regex.IsMatch(item.Key))
                {
                    keysToRemove.Add(item.Key);
                }
            }

            foreach (string key in keysToRemove)
            {
                Remove(key);
            }
        }

        /// <summary>
        /// 根据键值返回缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get { return Cache.Get(key); }
            set { Add(key, value); }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public void RemoveAll()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }
    }
}
