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
    public static class CQSSCPickerManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="collector"></param>
        public static IPicker GetInstance(string dataSource)
        {
            switch (dataSource)
            {
                case DataSource._168:
                    return (IPicker)Activator.CreateInstance(typeof(_168CQSSCPicker));
                case DataSource.KCZX:
                    return (IPicker)Activator.CreateInstance(typeof(KCCQSSCPicker));
                case DataSource.CJW:
                    return (IPicker)Activator.CreateInstance(typeof(CJWCQSSCPicker));
                default:
                    return (IPicker)Activator.CreateInstance(typeof(_168CQSSCPicker));
            }
        }

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <param name="threadCount">线程数量</param>
        public static List<OpenResult> Run(int threadCount)
        {
            Type type = typeof(DataSource);
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
