using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 数据库备份表
    /// </summary>
    public class SysDbBackup
    {
        /// <summary>
        /// 备份主键
        /// </summary>		
        public string id { get; set; }

        /// <summary>
        /// 备份类型
        /// </summary>		
        public string backup_type { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>		
        public string db_name { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>		
        public string file_name { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>		
        public string file_size { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>		
        public string file_path { get; set; }

        /// <summary>
        /// 备份时间
        /// </summary>		
        public DateTime backup_time { get; set; }

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

