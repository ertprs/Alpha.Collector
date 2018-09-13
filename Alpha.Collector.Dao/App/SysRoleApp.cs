/*******************************************************************************
 * Copyright © 2018 Robo 版权所有
 * Author: Tony Stack


*********************************************************************************/

using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Dao
{
    /// <inheritdoc />
    /// <summary>
    /// 角色App
    /// </summary>
    public class SysRoleApp 
    {
        private SysModuleApp moduleApp = new SysModuleApp();
        private SysModuleButtonApp moduleButtonApp = new SysModuleButtonApp();

        /// <summary>
        /// 读取角色列表
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

            sql += "and category = 1 order by sort_code";
            return MysqlHelper.GetList<SysRole>(sql, new { FullName = "%" + keyword + "%" });
        }

        /// <summary>
        /// 根据主键值获取角色
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysRole GetForm(string keyValue)
        {
            string sql = "select * from sys_role where id=@Id";
            return MysqlHelper.GetOne<SysRole>(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 删除指定角色
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

                sql = "delete from sys_roleauthorize where object_id=@Id";
                MysqlHelper.Execute(sql, new { Id = keyValue });
            }
        }

        /// <summary>
        /// 修改/新增角色
        /// </summary>
        /// <param name="model"></param>
        /// <param name="permissionIds"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysRole model, string[] permissionIds, string keyValue)
        {
            var moduleData = moduleApp.GetList();
            var buttonData = moduleButtonApp.GetList();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();

            //1、保存角色
            bool isAdd = string.IsNullOrEmpty(keyValue);
            string sql;
            if (!isAdd)
            {
                model.last_modify_time = DateTime.Now;
                model.last_modify_user = OperatorProvider.Provider.GetCurrent().UserId;
                sql = "update sys_role set organize_id=@organize_id, "
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
                    + "type, allow_edit, allow_delete, sort_code, delete_mark, enabled_mark, "
                    + "description, create_time, create_user) values (@id, @organize_id, @category, @en_code, @full_name, "
                    + "@type, @allow_edit, @allow_delete, @sort_code, @delete_mark, @enabled_mark, "
                    + "@description, @create_time, @create_user)";
                model.category = 1;
                model.id = PublicFunction.GUID();
                model.create_time = DateTime.Now;
                model.create_user = OperatorProvider.Provider.GetCurrent().UserId;
            }

            int result = MysqlHelper.Execute(sql, model);
            if (result < 0)
            {
                return;
            }

            //2、保存权限
            IList<SysRoleAuthorize> authList = new List<SysRoleAuthorize>();
            IList<SysModule> moduleList;
            IList<SysModuleButton> btnList;
            foreach (var itemId in permissionIds)
            {
                SysRoleAuthorize auth = new SysRoleAuthorize();
                auth.id = PublicFunction.GUID();
                auth.object_type = 1;
                auth.object_id = model.id;
                auth.item_id = itemId;
                auth.create_time = model.create_time;
                auth.create_user = model.create_user;

                moduleList = moduleData.Where(t => t.id == itemId).ToList();
                if (moduleList.Count != 0)
                {
                    auth.item_type = 1;
                }

                btnList = buttonData.Where(t => t.id == itemId).ToList();
                if (btnList.Count != 0)
                {
                    auth.item_type = 2;
                }

                authList.Add(auth);
            }

            sql = "delete from sys_roleauthorize where object_id=@ObjectId";
            result = MysqlHelper.Execute(sql, new { ObjectId = model.id });
            if (result < 0)
            {
                return;
            }

            sql = "insert into sys_roleauthorize (id, item_type, item_id, object_type, "
                + "object_id, sort_code, create_time, create_user) values(@id, @item_type, @item_id, @object_type, "
                + "@object_id, @sort_code, @create_time, @create_user)";
            MysqlHelper.Execute(sql, authList);
        }
    }
}
