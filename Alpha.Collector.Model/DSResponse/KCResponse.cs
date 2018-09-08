using System.Collections.Generic;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// KC返回结果
    /// </summary>
    public class KCResponse
    {
        /// <summary>
        /// 结果码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 结果消息
        /// </summary>
        public string StrCode { get; set; }

        /// <summary>
        /// 数据条数
        /// </summary>
        public int DataCount { get; set; }

        /// <summary>
        /// BackUrl
        /// </summary>
        public string BackUrl { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public List<KCResultInfo> BackData { get; set; }
    }

    /// <summary>
    /// 返回的数据
    /// </summary>
    public class KCResultInfo
    {
        /// <summary>
        /// 期数
        /// </summary>
        public string IssueNo { get; set; }

        /// <summary>
        /// 开奖号码
        /// </summary>
        public string LotteryOpen { get; set; }

        /// <summary>
        /// 开奖时间
        /// </summary>
        public string OpenTime { get; set; }
    }
}
