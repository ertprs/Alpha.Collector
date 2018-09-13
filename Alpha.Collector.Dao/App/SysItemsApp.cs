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
    /// 选项主表App
    /// </summary>
    public class SysItemsApp 
    {
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <returns></returns>
        public IList<SysItems> GetList()
        {
            string sql = "select * from sys_items where 1=1 ";
            return MysqlHelper.GetList<SysItems>(sql);
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysItems GetForm(string keyValue)
        {
            string sql = "select * from sys_items where id=@Id";
            return MysqlHelper.GetOne<SysItems>(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            string sql = "select 1 from sys_items where parent_id=@Id";
            IList<SysItems> list = MysqlHelper.GetList<SysItems>(sql, new { Id = keyValue });
            if (list.Count > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                sql = "delete from sys_items where id=@Id";
                MysqlHelper.Execute(sql, new { Id = keyValue });
            }
        }

        /// <summary>
        /// 更新/新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysItems model, string keyValue)
        {
            string sql;
            if (!string.IsNullOrEmpty(keyValue))
            {
                sql = "update sys_items set parent_id=@parent_id, en_code=@en_code, "
                    + "full_name=@full_name, is_tree=@is_tree, layers=@layers, sort_code=@sort_code, "
                    + "enabled_mark=@enabled_mark, description=@description, "
                    + "last_modify_time=@last_modify_time, "
                    + "last_modify_user=@last_modify_user where id=@id";
                model.last_modify_time = DateTime.Now;
                model.last_modify_user = OperatorProvider.Provider.GetCurrent().UserId;
            }
            else
            {
                sql = "insert into sys_items values (@id, @parent_id, @en_code, @full_name, "
                    + "@is_tree, @layers, @sort_code, @delete_mark, @enabled_mark, "
                    + "@description, @create_time, @create_user, @last_modify_time, "
                    + "@last_modify_user, @delete_time, @delete_user)";
                model.id = PublicFunction.GUID();
                model.create_time = DateTime.Now;
                model.create_user = OperatorProvider.Provider.GetCurrent().UserId;
            }

            MysqlHelper.Execute(sql, model);
        }
    }
}
