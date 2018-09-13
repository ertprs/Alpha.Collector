using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 模块表单实例
    /// </summary>
    public class SysModuleFormInstance
    {
        /// <summary>
        /// 表单实例主键
        /// </summary>		
        public string id { get; set; }
        
        /// <summary>
        /// 表单主键
        /// </summary>		
        public string form_id { get; set; }
        
        /// <summary>
        /// 对象主键
        /// </summary>		
        public string object_id { get; set; }
        
        /// <summary>
        /// 表单实例Json
        /// </summary>		
        public string instance_json { get; set; }
        
        /// <summary>
        /// 排序码
        /// </summary>		
        public int sort_code { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime create_time { get; set; }
        
        /// <summary>
        /// 创建用户
        /// </summary>		
        public string create_user { get; set; }
    }
}

