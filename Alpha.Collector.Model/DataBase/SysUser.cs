using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class SysUser
    {
        /// <summary>
        /// 用户主键
        /// </summary>		
        public string id { get; set; }

        /// <summary>
        /// 账户
        /// </summary>		
        public string account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>		
        public string real_name { get; set; }

        /// <summary>
        /// 呢称
        /// </summary>		
        public string nick_name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>		
        public string head_icon { get; set; }

        /// <summary>
        /// 性别
        /// </summary>		
        public bool gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>		
        public DateTime birthday { get; set; }

        /// <summary>
        /// 手机
        /// </summary>		
        public string mobile_phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>		
        public string email { get; set; }

        /// <summary>
        /// 微信
        /// </summary>		
        public string web_chat { get; set; }

        /// <summary>
        /// 主管主键
        /// </summary>		
        public string manager_id { get; set; }

        /// <summary>
        /// 安全级别
        /// </summary>		
        public int security_level { get; set; }

        /// <summary>
        /// 个性签名
        /// </summary>		
        public string signature { get; set; }

        /// <summary>
        /// 组织主键
        /// </summary>		
        public string organize_id { get; set; }

        /// <summary>
        /// 部门主键
        /// </summary>		
        public string department_id { get; set; }

        /// <summary>
        /// 角色主键
        /// </summary>		
        public string role_id { get; set; }

        /// <summary>
        /// 岗位主键
        /// </summary>		
        public string duty_id { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>		
        public bool is_administrator { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>		
        public int sort_code { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>		
        public bool? delete_mark { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>		
        public bool enabled_mark { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        public string description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime? create_time { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>		
        public string create_user { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>		
        public DateTime? last_modify_time { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>		
        public string last_modify_user { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>		
        public DateTime? delete_time { get; set; }

        /// <summary>
        /// 删除用户
        /// </summary>		
        public string delete_user { get; set; }
    }
}

