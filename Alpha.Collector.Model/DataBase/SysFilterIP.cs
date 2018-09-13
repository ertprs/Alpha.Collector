using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 过滤IP
    /// </summary>
    public class SysFilterIP
    {
        /// <summary>
        /// 过滤主键
        /// </summary>		
        public string id { get; set; }
        
        /// <summary>
        /// 类型
        /// </summary>		
        public bool type { get; set; }
        
        /// <summary>
        /// 开始IP
        /// </summary>		
        public string start_ip { get; set; }
        
        /// <summary>
        /// 结束IP
        /// </summary>		
        public string end_ip { get; set; }
        
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

