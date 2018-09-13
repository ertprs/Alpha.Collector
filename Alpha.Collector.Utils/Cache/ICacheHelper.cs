/*******************************************************************************
 * Copyright © 2018 Robo 版权所有
 * Author: Tony Stack


*********************************************************************************/
using System;

namespace Alpha.Collector.Utils
{
    /// <summary>
    /// 缓存帮助接口
    /// </summary>
    public interface ICacheHelper
    {
        /// <summary>
        /// 根据给定键值获取获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        T GetCache<T>(string cacheKey) where T : class;

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="cacheKey"></param>
        void WriteCache<T>(T value, string cacheKey) where T : class;

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="cacheKey"></param>
        /// <param name="expireTime"></param>
        void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class;

        /// <summary>
        /// 移除指定键值的缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        void RemoveCache(string cacheKey);

        /// <summary>
        /// 移除所有缓存
        /// </summary>
        void RemoveAllCache();
    }
}
