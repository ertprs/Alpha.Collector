using System;
using System.Collections.Generic;
namespace Alpha.Collector.Model
{
    public class ApiRequestParams
    {
        /// <summary>
        /// 客户Id
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 客户私钥
        /// </summary>
        public string CustomerKey { get; set; }

        /// <summary>
        /// 彩种代码
        /// </summary>
        public string LotteryType { get; set; }

        /// <summary>
        /// MD5
        /// </summary>
        public string MD5 { get; set; }
    }
}
