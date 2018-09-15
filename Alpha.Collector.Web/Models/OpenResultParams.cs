using System;

namespace Alpha.Collector.Web
{
    /// <summary>
    /// 开奖结果参数
    /// </summary>
    public class OpenResultParams
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
        /// 起始开奖时间
        /// </summary>
        public DateTime StartOpenTime { get; set; }

        /// <summary>
        /// 截止开奖时间
        /// </summary>
        public DateTime EndOpenTime { get; set; }

        /// <summary>
        /// 期号
        /// </summary>
        public string IssueNumber { get; set; }

        /// <summary>
        /// 是否合法
        /// </summary>
        public string IsLegal { get; set; }
    }
}