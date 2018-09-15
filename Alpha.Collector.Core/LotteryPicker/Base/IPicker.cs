using Alpha.Collector.Model;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 采集器接口
    /// </summary>
    public interface IPicker
    {
        /// <summary>
        /// 执行
        /// </summary>
        List<OpenResult> Run();

        /// <summary>
        /// 标识当前采集器是否有效
        /// </summary>
        bool IsValid { get; }
    }
}
