using System;

namespace Alpha.Collector.Web
{
    /// <summary>
    /// 错误日志参数
    /// </summary>
    public class AppLogParams
    {
        /// <summary>
        /// 彩种
        /// </summary>
        public string LotteryCode { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}