using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 组织表
    /// </summary>
    public class SysOrganize
    {
        /// <summary>
        /// 组织主键
        /// </summary>		
        public string id { get; set; }
        
        /// <summary>
        /// 父级
        /// </summary>		
        public string parent_id { get; set; }
        
        /// <summary>
        /// 层次
        /// </summary>		
        public int layers { get; set; }
        
        /// <summary>
        /// 编码
        /// </summary>		
        public string en_code { get; set; }
        
        /// <summary>
        /// 名称
        /// </summary>		
        public string full_name { get; set; }
        
        /// <summary>
        /// 简称
        /// </summary>		
        public string short_name { get; set; }
        
        /// <summary>
        /// 分类
        /// </summary>		
        public string category_id { get; set; }
        
        /// <summary>
        /// 负责人
        /// </summary>		
        public string manager_id { get; set; }
        
        /// <summary>
        /// 电话
        /// </summary>		
        public string tele_phone { get; set; }
        
        /// <summary>
        /// 手机
        /// </summary>		
        public string mobile_phone { get; set; }
        
        /// <summary>
        /// 微信
        /// </summary>		
        public string web_chat { get; set; }
        
        /// <summary>
        /// 传真
        /// </summary>		
        public string fax { get; set; }
        
        /// <summary>
        /// 邮箱
        /// </summary>		
        public string email { get; set; }
        
        /// <summary>
        /// 归属区域
        /// </summary>		
        public string area_id { get; set; }
        
        /// <summary>
        /// 联系地址
        /// </summary>		
        public string address { get; set; }
        
        /// <summary>
        /// 允许编辑
        /// </summary>		
        public bool allow_edit { get; set; }
        
        /// <summary>
        /// 允许删除
        /// </summary>		
        public bool allow_delete { get; set; }
        
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

