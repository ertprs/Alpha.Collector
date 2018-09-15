using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Utils
{
    /// <summary>
    /// 自定义的字典缓存
    /// </summary>
    public class DictionaryCache : ICache
    {
        private static Dictionary<string, KeyValuePair<object, DateTime>> dicData = new Dictionary<string, KeyValuePair<object, DateTime>>();

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            if (!this.Contains(key))
            {
                return default(T);
            }
            else
            {
                KeyValuePair<object, DateTime> keyValuePair = dicData[key];
                if (keyValuePair.Value < DateTime.Now)//过期
                {
                    this.Remove(key);
                    return default(T);
                }
                else
                {
                    return (T)keyValuePair.Key;
                }
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime">过期时间 默认30秒 </param>
        public void Add(string key, object data, long cacheTime = 30)
        {
            dicData.Add(key, new KeyValuePair<object, DateTime>(data, DateTime.Now.AddSeconds(cacheTime)));
        }

        /// <summary>
        /// 判断包含
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            return dicData.ContainsKey(key);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            dicData.Remove(key);
        }

        /// <summary>
        /// 全部删除
        /// </summary>
        public void RemoveAll()
        {
            dicData = new Dictionary<string, KeyValuePair<object, DateTime>>();
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                return this.Get<object>(key);
            }
            set
            {
                this.Add(key, value);
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            get
            {
                return dicData.Count(d => d.Value.Value > DateTime.Now);
            }
        }

        /// <summary>
        /// 取所有键
        /// </summary>
        public string GetAllKeys
        {
            get
            {
                return "";
            }
        }
    }
}
