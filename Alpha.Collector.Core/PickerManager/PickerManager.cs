using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 采集器管理器
    /// </summary>
    public class PickerManager : IPickerManager
    {
        private string _lotteryType;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lotteryType"></param>
        public PickerManager(string lotteryType)
        {
            this._lotteryType = lotteryType;
        }

        /// <summary>
        /// 获取采集器
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public IPicker GetPicker(string dataSource)
        {
            List<Type> list = LotteryHelper.GetTypeList(this._lotteryType);
            foreach (Type type in list)
            {
                try
                {
                    IPicker picker = Activator.CreateInstance(type) as IPicker;
                    if (picker != null && type.Name.ToLower().Contains(dataSource.ToLower()))
                    {
                        return picker;
                    }

                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取采集器集合
        /// </summary>
        /// <returns></returns>
        public List<IPicker> GetPickerList()
        {
            List<Type> list = LotteryHelper.GetTypeList(this._lotteryType);
            List<IPicker> pickerList = new List<IPicker>();
            foreach (Type type in list)
            {
                try
                {
                    IPicker picker = Activator.CreateInstance(type) as IPicker;
                    if (picker != null && picker.IsValid)
                    {
                        pickerList.Add(picker);
                    }

                }
                catch (Exception ex)
                {

                }
            }

            return pickerList;
        }

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <param name="taskCount">线程数量</param>
        public List<OpenResult> DoPick(int taskCount)
        {
            List<IPicker> pickerList = this.GetPickerList();
            if (pickerList.Count == 0)
            {
                return new List<OpenResult>();
            }

            List<OpenResult> list = new List<OpenResult>();
            ParallelOptions option = new ParallelOptions { MaxDegreeOfParallelism = Math.Min(taskCount, pickerList.Count) };
            Parallel.ForEach(pickerList, option, picker =>
            {
                list.AddRange(picker.Run());
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
