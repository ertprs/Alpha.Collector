using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 应用程序日志
    /// </summary>
    public class AppLog
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
        /// 数据源文字表示
        /// </summary>
        public string DataSourceText
        {
            get
            {
                return ModelFunction.GetDataSourceName(this.data_source);
            }
        }

        /// <summary>
        /// 格式化后的消息
        /// </summary>
        public string FormatMessage
        {
            get
            {
                return this.log_message.Length > 100 ? this.log_message.Substring(0, 100) + "..." : this.log_message;
            }
        }

        /// <summary>
        /// 重写ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"数据源：{this.data_source ?? ""}，彩种：{this.lottery_code}，时间：{this.create_time}，消息：{this.log_message}";
        }
    }
}
