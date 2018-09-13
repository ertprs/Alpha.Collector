using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public class SysLog
    {
        /// <summary>
        /// 日志主键
        /// </summary>		
        public string id { get; set; }

        /// <summary>
        /// 日期
        /// </summary>		
        public DateTime create_time { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>		
        public string account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>		
        public string nick_name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>		
        public string type { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>		
        public string ip { get; set; }

        /// <summary>
        /// IP所在城市
        /// </summary>		
        public string ip_address { get; set; }

        /// <summary>
        /// 系统模块Id
        /// </summary>		
        public string module_id { get; set; }

        /// <summary>
        /// 系统模块
        /// </summary>		
        public string module_name { get; set; }

        /// <summary>
        /// 结果
        /// </summary>		
        public bool result { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        public string description { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>		
        public string create_user { get; set; }
    }
}


