using Alpha.Collector.Model;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 采集器管理器接口
    /// </summary>
    public interface IPickerManager
    {
        /// <summary>
        /// 获取采集器
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        IPicker GetPicker(string dataSource);

        /// <summary>
        /// 执行多采集器抓取，多线程抓取
        /// </summary>
        /// <param name="taskCount">线程的数量</param>
        /// <returns></returns>
        List<OpenResult> DoPick(int taskCount);
    }
}
