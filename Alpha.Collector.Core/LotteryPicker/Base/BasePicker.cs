using Alpha.Collector.Dao;
using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 采集器基类
    /// </summary>
    public class BasePicker
    {
        /// <summary>
        /// 需要采集的彩种列表
        /// </summary>
        protected List<string> LotteryList
        {
            get
            {
                try
                {
                    List<Lottery> list = CacheManager.GetOrSet(CacheKey.LOTTERY_KEY, () =>
                    {
                        return LotteryApp.GetList();
                    }, 24 * 60 * 60);
                    return (from l in list.Where(l => l.status == 1) select l.code).ToList();
                }
                catch (Exception ex)
                {
                    return new List<string>();
                }
            }
        }

        /// <summary>
        /// 采集中的数据源列表
        /// </summary>
        protected List<string> DataSourceList
        {
            get
            {
                try
                {
                    List<DataSource> list = CacheManager.GetOrSet(CacheKey.DATASOURCE_KEY, () =>
                    {
                        return DataSourceApp.GetList();
                    }, 24 * 60 * 60);
                    return (from l in list.Where(l => l.status == 1) select l.code).ToList();
                }
                catch (Exception ex)
                {
                    return new List<string>();
                }
            }
        }
    }
}
