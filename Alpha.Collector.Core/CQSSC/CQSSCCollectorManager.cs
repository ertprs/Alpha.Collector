using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 重庆时时彩采集器管理器
    /// </summary>
    public static class CQSSCCollectorManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="collector"></param>
        public static ICQSSCCollector GetInstance(string dataSource)
        {
            switch (dataSource)
            {
                case DataSourceEnum._168:
                    return (ICQSSCCollector)Activator.CreateInstance(typeof(_168CQSSHCollector));
                case DataSourceEnum.KCZX:
                    return (ICQSSCCollector)Activator.CreateInstance(typeof(KCCQSSCCollector));
                case DataSourceEnum.CJW:
                    return (ICQSSCCollector)Activator.CreateInstance(typeof(CJWCQSSCCollector));
                default:
                    return (ICQSSCCollector)Activator.CreateInstance(typeof(_168CQSSHCollector));
            }
        }

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <param name="threadCount">线程数量</param>
        public static List<OpenResult> Run(int threadCount)
        {
            Type type = typeof(DataSourceEnum);
            List<FieldInfo> fieldInfo = type.GetFields().ToList();

            List<OpenResult> list = new List<OpenResult>();
            ParallelOptions option = new ParallelOptions { MaxDegreeOfParallelism = Math.Min(threadCount, fieldInfo.Count) };
            Parallel.ForEach(fieldInfo, option, field =>
            {
                string dataSource = field.GetValue(null).ToString();
                list.AddRange(GetInstance(dataSource).Run());
            });

            return list.DistinctEx().OrderBy(o => o.issue_number).ToList();
        }

        /// <summary>
        /// 去重
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<OpenResult> DistinctEx(this List<OpenResult> list)
        {
            List<OpenResult> newList = new List<OpenResult>();
            foreach (OpenResult result in list)
            {
                if (newList.Exists(o => o.issue_number == result.issue_number && o.lottery_code == result.lottery_code))
                {
                    continue;
                }

                newList.Add(result);
            }

            return newList;
        }
    }
}
