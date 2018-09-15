using Alpha.Collector.Dao;
using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 采集器管理器
    /// </summary>
    public abstract class PickerManager : IPickerManager
    {
        /// <summary>
        /// 获取采集器
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public abstract IPicker GetPicker(string dataSource);

        /// <summary>
        /// 获取采集器集合
        /// </summary>
        /// <returns></returns>
        public abstract List<IPicker> GetPickerList();

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <param name="taskCount">线程数量</param>
        public List<OpenResult> DoPick(int taskCount)
        {
            //取数据源开关
            List<DataSource> dataSourceList = CacheManager.GetOrSet(CacheKey.DATASOURCE_KEY, () =>
            {
                return DataSourceApp.GetList().Where(d => d.status == 1).ToList();
            }, 7 * 24 * 60 * 60);

            if (dataSourceList.Count == 0)
            {
                throw new Exception($"没有可用的数据源");
            }

            string dataSourceStr = dataSourceList.Aggregate(string.Empty, (c, d) => c + d.name + ",").TrimEnd(',');

            Type type = typeof(DataSourceEnum);
            List<FieldInfo> fieldInfo = type.GetFields().ToList();

            List<OpenResult> list = new List<OpenResult>();
            ParallelOptions option = new ParallelOptions { MaxDegreeOfParallelism = Math.Min(taskCount, fieldInfo.Count) };
            Parallel.ForEach(fieldInfo, option, field =>
            {
                string dataSource = field.GetValue(null).ToString();
                if (dataSourceStr.Contains(dataSource))
                {
                    IPicker picker = this.GetPicker(dataSource);
                    if (picker != null)
                    {
                        list.AddRange(picker.Run());
                    }
                }
            });

            list = DistinctEx(list);
            return list.OrderBy(o => o.issue_number).ToList();
        }

        /// <summary>
        /// 去重
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<OpenResult> DistinctEx(List<OpenResult> list)
        {
            List<OpenResult> newList = new List<OpenResult>();
            foreach (OpenResult result in list)
            {
                if (newList.Exists(o => o.issue_number == result.issue_number && o.lottery_code == result.lottery_code && o.data_source == result.data_source))
                {
                    continue;
                }

                newList.Add(result);
            }

            return newList;
        }
    }
}
