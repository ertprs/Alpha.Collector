using Alpha.Collector.Utils.Attributes;
using System;

namespace Alpha.Collector.Model.DataBase
{
    /// <summary>
    /// 彩票开奖结果
    /// </summary>
    public class OpenResult
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 采集时间戳
        /// </summary>
        public long create_timestamp
        {
            get
            {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return (long)(this.create_time - startTime).TotalMilliseconds;
            }
        }

        /// <summary>
        /// 开奖时间
        /// </summary>
        public DateTime open_time { get; set; }

        /// <summary>
        /// 开奖时间戳
        /// </summary>
        public long open_timestamp
        {
            get
            {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return (long)(this.open_time - startTime).TotalMilliseconds;
            }
        }

        /// <summary>
        /// 彩种代码
        /// </summary>
        public string lottery_code { get; set; }

        /// <summary>
        /// 期号
        /// </summary>
        public long issue_number { get; set; }

        /// <summary>
        /// 开奖号码
        /// </summary>
        public string open_data { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public string data_source { get; set; }
    }
}
