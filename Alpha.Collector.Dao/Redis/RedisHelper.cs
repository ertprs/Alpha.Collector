using Alpha.Collector.Utils;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Dao
{
    /// <summary>
    /// RedisHelper
    /// </summary>
    public partial class RedisHelper
    {
        public const string DefaultOrder = "desc";

        #region Keys

        /// <summary>
        /// 指定键是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyExists(string key)
        {
            return Cache.KeyExists(key);
        }

        /// <summary>
        /// 设置绝对过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static bool SetExpire(string key, DateTime datetime)
        {
            return Cache.KeyExpire(key, datetime);
        }

        /// <summary>
        /// 设置相对过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static bool SetExpire(string key, int timeout = 0)
        {
            return Cache.KeyExpire(key, DateTime.Now.AddSeconds(timeout));
        }

        /// <summary>
        /// 设置缓存，如果timeout参数大于0则添加过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static bool Set<T>(string key, T t, int timeout = 0)
        {
            var value = t.ToJson();
            bool result = Cache.StringSet(key, value);
            if (timeout > 0)
            {
                Cache.KeyExpire(key, DateTime.Now.AddSeconds(timeout));
            }
            return result;
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyDelete(string key)
        {
            return Cache.KeyDelete(key);
        }

        /// <summary>
        /// 重命名键值
        /// </summary>
        /// <param name="oldKey"></param>
        /// <param name="newKey"></param>
        /// <returns></returns>
        public static bool KeyRename(string oldKey, string newKey)
        {
            return Cache.KeyRename(oldKey, newKey);
        }

        #endregion

        #region Hash表

        /// <summary>
        /// 指定键值在Hash表中是否存在
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsExist(string hashId, string key)
        {
            return Cache.HashExists(hashId, key);
        }

        /// <summary>
        /// 将值设置到Hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool SetHash<T>(string hashId, string key, T t)
        {
            return Cache.HashSet(hashId, key, t.ToJson());
        }

        /// <summary>
        /// 从Hash表移除指定数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(string hashId, string key)
        {
            return Cache.HashDelete(hashId, key);
        }

        /// <summary>
        /// 设置Hash表自增
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long StringIncrement(string hashId, string key, long value = 1)
        {
            return Cache.HashIncrement(hashId, key, value);
        }

        /// <summary>
        /// 从Hash表获取指定数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string hashId, string key)
        {
            string value = Cache.HashGet(hashId, key);
            return value.ToEntity<T>();
        }

        /// <summary>
        /// 获取指定Hash表的数量
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public static long GetHashCount(string hashId)
        {
            return Cache.HashLength(hashId);
        }

        /// <summary>
        /// 从Hash表获取指定数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string hashId, string key)
        {
            return Cache.HashGet(hashId, key).ToString();
        }

        /// <summary>
        /// 从Hash表获取列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public static List<T> GetAll<T>(string hashId)
        {
            var result = new List<T>();
            var list = Cache.HashGetAll(hashId).ToList();
            if (list.Count > 0)
            {
                list.ForEach(x =>
                {
                    result.Add(x.Value.ToString().ToEntity<T>());
                });
            }
            return result;
        }

        /// <summary>
        /// 获取所有Hash表键值
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public static List<string> GetAllFields(string hashId)
        {
            var result = new List<string>();
            var list = Cache.HashKeys(hashId).ToList();
            if (list.Count > 0)
            {
                list.ForEach(x =>
                {
                    result.Add(x.ToJson());
                });
            }
            return result;
        }

        #endregion

        #region 有序集合

        /// <summary>
        /// 指定有续集合是否存在
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool SortedSetItemIsExist(string setId, string item)
        {
            return GetItemScoreFromSortedSet(setId, item) != null;
        }

        /// <summary>
        /// 添加指定数据到有序集合
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        /// <param name="score"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static bool SortedSetAdd(string setId, string item, double score, int timeout = 0)
        {
            return Cache.SortedSetAdd(setId, item, score);
        }

        /// <summary>
        /// 获取有序集合列表
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="fromRank"></param>
        /// <param name="toRank"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static List<string> GetSortedSetRangeByRank(string setId, long fromRank, long toRank, string order = DefaultOrder)
        {
            var result = new List<string>();
            var list = Cache.SortedSetRangeByRank(setId, fromRank, toRank, order == Order.Descending.ToString().ToLower() ? Order.Descending : Order.Ascending).ToList();
            if (list.Any())
            {
                list.ForEach(x =>
                {
                    result.Add(x.ToJson());
                });
            }
            return result;
        }

        /// <summary>
        /// 获取有序集合
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="fromRank"></param>
        /// <param name="toRank"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static Dictionary<string, double> GetSortedSetRangeByRankWithScores(string setId, long fromRank, long toRank, string order = DefaultOrder)
        {
            var result = new Dictionary<string, double>();
            var list = Cache.SortedSetRangeByRankWithScores(setId, fromRank, toRank, order == Order.Descending.ToString().ToLower() ? Order.Descending : Order.Ascending).ToList();
            if (list.Any())
            {
                list.ForEach(x =>
                {
                    result.Add(x.Element, x.Score);
                });
            }
            return result;
        }

        /// <summary>
        /// 获取有序集合
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static List<string> GetSortedSetRangeByValue(string setId, long minValue, long maxValue)
        {
            var result = new List<string>();
            var list = Cache.SortedSetRangeByValue(setId, minValue, maxValue).ToList();
            if (list.Any())
            {
                list.ForEach(x =>
                {
                    result.Add(x.ToJson());
                });
            }
            return result;
        }

        /// <summary>
        /// 获取有序集合长度
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        public static long GetSortedSetLength(string setId)
        {
            return Cache.SortedSetLength(setId);
        }

        /// <summary>
        /// 设置有序集合长度
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static long GetSortedSetLength(string setId, double minValue, double maxValue)
        {
            return Cache.SortedSetLength(setId, minValue, maxValue);
        }

        /// <summary>
        /// 设置有序集合
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static long? GetItemRankFromSortedSet(string setId, string item, string order = DefaultOrder)
        {
            return Cache.SortedSetRank(setId, item, order == Order.Descending.ToString().ToLower() ? Order.Descending : Order.Ascending);
        }

        /// <summary>
        /// 设置有序集合排序
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static double? GetItemScoreFromSortedSet(string setId, string item)
        {
            return Cache.SortedSetScore(setId, item);
        }

        /// <summary>
        /// 设置有序集合排序方式为升序
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static double SetSortedSetItemIncrement(string setId, string item, double score = 1)
        {
            return Cache.SortedSetIncrement(setId, item, score);
        }

        /// <summary>
        /// 设置有序集合排序方式为降序
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static double SortedSetItemDecrement(string setId, string item, double score = -1)
        {
            return Cache.SortedSetDecrement(setId, item, score);
        }

        /// <summary>
        /// 删除有序集合
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool RemoveItemFromSortedSet(string setId, string item)
        {
            return Cache.SortedSetRemove(setId, item);
        }

        /// <summary>
        /// 删除有序集合
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="fromRank"></param>
        /// <param name="toRank"></param>
        /// <returns></returns>
        public static long RemoveByRankFromSortedSet(string setId, long fromRank, long toRank)
        {
            return Cache.SortedSetRemoveRangeByRank(setId, fromRank, toRank);
        }

        /// <summary>
        /// 删除有序集合
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static long RemoveByScoreFromSortedSet(string setId, double minValue, double maxValue)
        {
            return Cache.SortedSetRemoveRangeByScore(setId, minValue, maxValue);
        }

        #endregion

        #region 列表

        /// <summary>
        /// 添加至列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static long AddList<T>(string listId, T t)
        {
            return Cache.ListLeftPush(listId, t.ToJson());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static List<T> GetList<T>(string listId, long start = 0, long stop = -1)
        {
            var result = new List<T>();
            var list = Cache.ListRange(listId, start, stop).ToList();
            if (list.Count > 0)
            {
                list.ForEach(x =>
                {
                    result.Add(x.ToString().ToEntity<T>());
                });
            }
            return result;
        }
        #endregion

        #region 字符串

        /// <summary>
        /// 获取指定值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        public static string Get(string key)
        {
            return Cache.StringGet(key);
        }

        /// <summary>
        /// 获取指定值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            return Cache.StringGet(key).ToString().ToEntity<T>();
        }

        /// <summary>
        /// 设置自增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double StringIncrement(string key, double value)
        {
            return Cache.StringIncrement(key, value);
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long StringAppend(string key, string value)
        {
            return Cache.StringAppend(value, value, CommandFlags.None);
        }

        #endregion
    }
}
