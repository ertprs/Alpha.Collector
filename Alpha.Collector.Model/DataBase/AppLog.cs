using Newtonsoft.Json;
using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 应用程序日志
    /// </summary>
    public class AppLog
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public long id { get; set; }

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
        /// 日志类型
        /// </summary>
        public string log_type { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string log_message { get; set; }

        /// <summary>
        /// 对应彩种
        /// </summary>
        public string lottery_code { get; set; }

        /// <summary>
        /// 对应数据源
        /// </summary>
        public string data_source { get; set; }

        /// <summary>
        /// 重写
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
