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
    /// 组织App
    /// </summary>
    public class SysOrganizeApp 
    {
        /// <summary>
        /// 获取组织列表
        /// </summary>
        /// <returns></returns>
        public IList<SysOrganize> GetList()
        {
            string sql = "select * from sys_organize order by create_time";
            return MysqlHelper.GetList<SysOrganize>(sql);
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysOrganize GetForm(string keyValue)
        {
            string sql = "select * from sys_organize where id=@Id";
            return MysqlHelper.GetOne<SysOrganize>(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {

            string sql = "select 1 from sys_organize where parent_id=@Id";
            IList<SysOrganize> list = MysqlHelper.GetList<SysOrganize>(sql, new { Id = keyValue });
            if (list.Count > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                sql = "delete from sys_organize where id=@Id";
                MysqlHelper.Execute(sql, new { Id = keyValue });
            }
        }

        /// <summary>
        /// 更新/新增实体
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysOrganize model, string keyValue)
        {
            string sql;
            if (!string.IsNullOrEmpty(keyValue))
            {
                sql = "update sys_organize set parent_id=@parent_id, layers=@layers, en_code=@en_code, "
                    + "full_name=@full_name, short_name=@short_name, category_id=@category_id, "
                    + "manager_id=@manager_id, tele_phone=@tele_phone, mobile_phone=@mobile_phone, "
                    + "web_chat=@web_chat, fax=@fax, email=@email, area_id=@area_id, "
                    + "address=@address, allow_edit=@allow_edit, allow_delete=@allow_delete, "
                    + "sort_code=@sort_code, last_modify_time=@last_modify_time, enabled_mark=@enabled_mark, "
                    + "last_modify_user=@last_modify_user where id=@id";
                model.last_modify_time = DateTime.Now;
                model.last_modify_user = OperatorProvider.Provider.GetCurrent().UserId;
            }
            else
            {
                sql = "insert into sys_organize values (@id, @parent_id, @layers, @en_code, @full_name, "
                    + "@short_name, @category_id, @manager_id, @tele_phone, @mobile_phone, @web_chat, "
                    + "@fax, @email,@area_id, @address, @allow_edit, @allow_delete, @sort_code, "
                    + "@delete_mark, @enabled_mark, @description, "
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
