using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 必发返回结果
    /// </summary>
    public class BiFaResponse
    {
        /// <summary>
        /// 期数
        /// </summary>
        public long turnNum { get; set; }

        /// <summary>
        /// 开奖号码
        /// </summary>
        public string openNum { get; set; }

        /// <summary>
        /// 开奖时间
        /// </summary>
        public DateTime openTime { get; set; }
    }
}
