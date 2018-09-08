using System.Collections.Generic;

namespace Alpha.Collector.Model._168
{
    /// <summary>
    /// 168开奖网返回结果
    /// </summary>
    public class _168Response<T>
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
        public _168Result<T> result { get; set; }
    }

    /// <summary>
    /// 168开奖网开奖结果
    /// </summary>
    public class _168Result<T>
    {
        /// <summary>
        /// 商务号
        /// </summary>
        public int businessCode { get; set; }

        /// <summary>
        /// 开奖数据
        /// </summary>
        public List<T> data { get; set; }
    }
}
