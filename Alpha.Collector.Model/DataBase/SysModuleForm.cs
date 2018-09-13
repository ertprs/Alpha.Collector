using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 模块表单
    /// </summary>
    public class SysModuleForm 
    {
        /// <summary>
        /// 表单主键
        /// </summary>		
        public string id { get; set; }

        /// <summary>
        /// 模块主键
        /// </summary>		
        public string module_id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>		
        public string en_code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>		
        public string full_name { get; set; }

        /// <summary>
        /// 表单控件Json
        /// </summary>		
        public string form_json { get; set; }

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

