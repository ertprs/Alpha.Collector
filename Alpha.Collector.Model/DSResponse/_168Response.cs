using System.Collections.Generic;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 168开奖网返回结果
    /// </summary>
    public class _168Response
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int errorCode { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 开奖结果
        /// </summary>
        public _168Result result { get; set; }
    }

    /// <summary>
    /// 168开奖网开奖结果
    /// </summary>
    public class _168Result
    {
        /// <summary>
        /// 商务号
        /// </summary>
        public int businessCode { get; set; }

        /// <summary>
        /// 开奖数据
        /// </summary>
        public List<_168Data> data { get; set; }
    }

    /// <summary>
    /// 168开奖网开奖数据
    /// </summary>
    public class _168Data
    {
        /// <summary>
        /// 开奖号码
        /// </summary>
        public string preDrawCode { get; set; }

        /// <summary>
        /// 开奖时间
        /// </summary>
        public string preDrawTime { get; set; }

        /// <summary>
        /// 期号
        /// </summary>
        public string preDrawIssue { get; set; }
    }
}
