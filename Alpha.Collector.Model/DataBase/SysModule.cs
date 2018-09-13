using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 系统模块
    /// </summary>
    public class SysModule
    {
        /// <summary>
        /// 模块主键
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
        /// 图标
        /// </summary>		
        public string icon { get; set; }

        /// <summary>
        /// 连接
        /// </summary>		
        public string url_address { get; set; }

        /// <summary>
        /// 目标
        /// </summary>		
        public string target { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>		
        public bool is_menu { get; set; }

        /// <summary>
        /// 展开
        /// </summary>		
        public bool is_expand { get; set; }

        /// <summary>
        /// 公共
        /// </summary>		
        public bool is_public { get; set; }

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
        /// 创建日期
        /// </summary>		
        public DateTime? create_time { get; set; }

        /// <summary>
        /// 创建用户主键
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

