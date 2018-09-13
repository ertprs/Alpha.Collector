using System;

namespace Alpha.Collector.Utils
{
    /// <summary>
    /// 当前登录用户
    /// </summary>
    public class OperatorModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户代号
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// 用户所属的公司ID
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 用户所属部门ID
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 用户所属角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 用户登录的IP地址
        /// </summary>
        public string LoginIPAddress { get; set; }

        /// <summary>
        /// 用户登录的IP的地名
        /// </summary>
        public string LoginIPAddressName { get; set; }

        public string LoginToken { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 是否系统管理员
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}

