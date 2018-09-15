using System;
using System.Collections.Generic;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 多宝返回结果
    /// </summary>
    public class DuoBaoReponse
    {
        public string c { get; set; }
        public List<DuobaoModel> list { get; set; }
    }

    public class DuobaoModel
    {
        /// <summary>
        /// 期号
        /// </summary>
        public long c_t { get; set; }

        /// <summary>
        /// 开奖时间
        /// </summary>
        public DateTime c_d { get; set; }

        /// <summary>
        /// 开奖号码
        /// </summary>
        public string c_r { get; set; } 
    }
}
