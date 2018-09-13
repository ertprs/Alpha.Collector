using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// 角色授权表
    /// </summary>
    public class SysRoleAuthorize
    {
        /// <summary>
        /// 角色授权主键
        /// </summary>		
        public string id { get; set; }

        /// <summary>
        /// 项目类型1-模块2-按钮3-列表
        /// </summary>		
        public int item_type { get; set; }

        /// <summary>
        /// 项目主键
        /// </summary>		
        public string item_id { get; set; }

        /// <summary>
        /// 对象分类1-角色2-部门-3用户
        /// </summary>		
        public int object_type { get; set; }

        /// <summary>
        /// 对象主键
        /// </summary>		
        public string object_id { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>		
        public int sort_code { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime? create_time { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>		
        public string create_user { get; set; }
    }
}

