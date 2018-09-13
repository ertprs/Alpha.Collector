using Alpha.Collector.Model;
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
        /// 执行抓取
        /// </summary>
        /// <param name="taskCount">线程数量</param>
        List<OpenResult> IPickerManager.DoPick(int taskCount)
        {
            Type type = typeof(DataSource);
            List<FieldInfo> fieldInfo = type.GetFields().ToList();

            List<OpenResult> list = new List<OpenResult>();
            ParallelOptions option = new ParallelOptions { MaxDegreeOfParallelism = Math.Min(taskCount, fieldInfo.Count) };
            Parallel.ForEach(fieldInfo, option, field =>
            {
                string dataSource = field.GetValue(null).ToString();
                IPicker picker = this.GetPicker(dataSource);
                if (picker != null)
                {
                    list.AddRange(picker.Run());
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
