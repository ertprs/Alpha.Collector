using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 彩种
    /// </summary>
    public class Lottery
    {
        /// <summary>
        /// ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 创建时间戳
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
        /// 修改时间
        /// </summary>
        public DateTime update_time { get; set; }

        /// <summary>
        /// 修改时间戳
        /// </summary>
        public long update_timestamp
        {
            get
            {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return (long)(this.update_time - startTime).TotalMilliseconds;
            }
        }

        /// <summary>
        /// 彩种名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 彩种代码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 采集状态
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string StatusTest
        {
            get
            {
                return this.status == 1 ? "抓取中" : "停止中";
            }
        }
    }
}
