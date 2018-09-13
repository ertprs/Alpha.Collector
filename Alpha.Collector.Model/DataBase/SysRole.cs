using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class SysRole
    {
        /// <summary>
        /// 角色主键
        /// </summary>		
        public string id { get; set; }

        /// <summary>
        /// 组织主键
        /// </summary>		
        public string organize_id { get; set; }

        /// <summary>
        /// 分类:1-角色2-岗位
        /// </summary>		
        public int category { get; set; }

        /// <summary>
        /// 编号
        /// </summary>		
        public string en_code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>		
        public string full_name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>		
        public string type { get; set; }

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

