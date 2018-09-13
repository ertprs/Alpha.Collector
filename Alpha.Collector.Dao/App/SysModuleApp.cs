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
    /// 模块App
    /// </summary>
    public class SysModuleApp 
    {
        /// <summary>
        /// 获取所有的模块
        /// </summary>
        /// <returns></returns>
        public IList<SysModule> GetList()
        {
            string sql = "select * from sys_module where 1=1";
            return MysqlHelper.GetList<SysModule>(sql);    
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysModule GetForm(string keyValue)
        {
            string sql = "select * from sys_module where id=@Id";
            return MysqlHelper.GetOne<SysModule>(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            string sql = "select 1 from sys_module where parent_id=@Id";
            IList<SysModule> list = MysqlHelper.GetList<SysModule>(sql, new { Id = keyValue });
            if (list.Count > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                sql = "delete from sys_module where id=@Id";
                MysqlHelper.Execute(sql, new { Id = keyValue });
            }
        }

        /// <summary>
        /// 更新/删除
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysModule model, string keyValue)
        {
            string sql;
            if (!string.IsNullOrEmpty(keyValue))
            {
                sql = "update sys_module set parent_id=@parent_id, layers=@layers, en_code=@en_code, "
                    + "full_name=@full_name, icon=@icon, url_address=@url_address, "
                    + "target=@target, is_menu=@is_menu, is_expand=@is_expand, is_public=@is_public, "
                    + "allow_edit=@allow_edit, allow_delete=@allow_delete, sort_code=@sort_code, "
                    + "enabled_mark=@enabled_mark, description=@description, last_modify_time=@last_modify_time, "
                    + "last_modify_user=@last_modify_user where id=@id";
                model.last_modify_time = DateTime.Now;
                model.last_modify_user = OperatorProvider.Provider.GetCurrent().UserId;
            }
            else
            {
                sql = "insert into sys_module values (@id, @parent_id, @layers, @en_code, @full_name, "
                    + "@icon, @url_address, @target, @is_menu, @is_expand, @is_public, @allow_edit, "
                    + "@allow_delete, @sort_code, @delete_mark, @enabled_mark, @description, "
                    + "@create_time, @create_user, @last_modify_time, @last_modify_user, "
                    + "@delete_time, @delete_user)";
                model.id = PublicFunction.GUID();
                model.create_time = DateTime.Now;
                model.create_user = OperatorProvider.Provider.GetCurrent().UserId;
            }

            MysqlHelper.Execute(sql, model);
        }
    }
}
