/*******************************************************************************
 * Copyright © 2018 Robo 版权所有
 * Author: Tony Stack


*********************************************************************************/

namespace Alpha.Collector.Utils
{
    /// <summary>
    /// 缓存工程类
    /// </summary>
    public class CacheFactory
    {
        /// <summary>
        /// 新建缓存帮助类
        /// </summary>
        /// <returns></returns>
        public static ICacheHelper CreateCacheHelper()
        {
            return new CacheHelper();
        }
    }
}
