/*******************************************************************************
 * Copyright © 2018 Robo 版权所有
 * Author: Tony Stack


*********************************************************************************/

using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Dao
{
    /// <summary>
    /// 职责
    /// </summary>
    public class SysDutyApp
    {
        /// <summary>
        /// 获取职责列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysRole> GetList(string keyword = "")
        {
            string sql = "select * from sys_role where 1=1 ";
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += "and (full_name like @FullName or en_code like @FullName) ";
            }

            sql += "and category = 2 order by sort_code";

            return MysqlHelper.GetList<SysRole>(sql, new { FullName = "%" + keyword + "%" });
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysRole GetForm(string keyValue)
        {
            string sql = "select * from sys_role where id=@Id";
            return MysqlHelper.GetOne<SysRole>(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            string sql = "select 1 from sys_user where role_id=@Id";
            IList<SysUser> list = MysqlHelper.GetList<SysUser>(sql, new { Id = keyValue });
            if (list.Count > 0)
            {
                throw new Exception("删除失败！操作的角色包含了用户，请先删除这些用户或更改这些用户所属角色。");
            }
            else
            {
                sql = "delete from sys_role where id=@Id";
                MysqlHelper.Execute(sql, new { Id = keyValue });
            }
        }

        /// <summary>
        /// 更新/新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysRole model, string keyValue)
        {
            //1、保存角色
            bool isAdd = string.IsNullOrEmpty(keyValue);
            string sql;
            if (!isAdd)
            {
                sql = "update sys_role set organize_id=@organize_id, category=@category, "
                    + "en_code=@en_code, full_name=@full_name, type=@type, allow_edit=@allow_edit, "
                    + "allow_delete=@allow_delete, sort_code=@sort_code, enabled_mark=@enabled_mark,  "
                    + "description=@description, last_modify_time=@last_modify_time, "
                    + "last_modify_user=@last_modify_user where id=@id";
                model.last_modify_time = DateTime.Now;
                model.last_modify_user = OperatorProvider.Provider.GetCurrent().UserId;
            }
            else
            {
                sql = "insert into sys_role (id, organize_id, category, en_code, full_name, "
                    + "type, allow_edit, allow_delete, sort_code,  "
                    + "description, create_time, create_user) values (@id, @organize_id, @category, @en_code, @full_name, "
                    + "@type, @allow_edit, @allow_delete, @sort_code,  "
                    + "@description, @create_time, @create_user)";
                model.category = 2;
                model.id = PublicFunction.GUID();
                model.create_time = DateTime.Now;
                model.create_user = OperatorProvider.Provider.GetCurrent().UserId;
            }

            MysqlHelper.Execute(sql, model);
        }
    }
}
