using Alpha.Collector.Model;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 重庆时时彩采集器接口
    /// </summary>
    public interface ICQSSCCollector
    {
        /// <summary>
        /// 执行
        /// </summary>
        List<OpenResult> Run();
    }
}
