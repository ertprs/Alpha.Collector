using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 用户登录信息表
    /// </summary>
    public class SysUserLogOn
    {
        /// <summary>
        /// 用户登录主键
        /// </summary>		
        public string id { get; set; }

        /// <summary>
        /// 用户主键
        /// </summary>		
        public string user_id { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>		
        public string user_password { get; set; }

        /// <summary>
        /// 用户秘钥
        /// </summary>		
        public string user_secretkey { get; set; }

        /// <summary>
        /// 允许登录时间开始
        /// </summary>		
        public DateTime allow_start_time { get; set; }

        /// <summary>
        /// 允许登录时间结束
        /// </summary>		
        public DateTime allow_end_time { get; set; }

        /// <summary>
        /// 暂停用户开始日期
        /// </summary>		
        public DateTime lock_start_date { get; set; }

        /// <summary>
        /// 暂停用户结束日期
        /// </summary>		
        public DateTime lock_end_date { get; set; }

        /// <summary>
        /// 第一次访问时间
        /// </summary>		
        public DateTime first_visit_time { get; set; }

        /// <summary>
        /// 上一次访问时间
        /// </summary>		
        public DateTime previous_visit_time { get; set; }

        /// <summary>
        /// 最后访问时间
        /// </summary>		
        public DateTime last_visit_time { get; set; }

        /// <summary>
        /// 最后修改密码日期
        /// </summary>		
        public DateTime change_password_date { get; set; }

        /// <summary>
        /// 允许同时有多用户登录
        /// </summary>		
        public bool multi_user_login { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>		
        public int log_on_count { get; set; }

        /// <summary>
        /// 在线状态
        /// </summary>		
        public bool user_on_line { get; set; }

        /// <summary>
        /// 密码提示问题
        /// </summary>		
        public string question { get; set; }

        /// <summary>
        /// 密码提示答案
        /// </summary>		
        public string answer_question { get; set; }

        /// <summary>
        /// 是否访问限制
        /// </summary>		
        public bool check_ip_address { get; set; }

        /// <summary>
        /// 系统语言
        /// </summary>		
        public string language { get; set; }

        /// <summary>
        /// 系统样式
        /// </summary>		
        public string theme { get; set; }
    }
}

