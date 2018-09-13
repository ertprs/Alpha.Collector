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
    /// 选项明细App
    /// </summary>
    public class SysItemsDetailApp
    {
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysItemsDetail> GetList(string itemId = "", string keyword = "")
        {
            string sql = "select * from sys_itemsdetail where 1=1 ";
            if (!string.IsNullOrEmpty(itemId))
            {
                sql += "and item_id=@ItemId ";
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                sql += "and (item_name like @Keyword or item_code like @Keyword) ";
            }

            sql += "order by sort_code";

            return MysqlHelper.GetList<SysItemsDetail>(sql, new { ItemId = itemId, Keyword = "%" + keyword + "%" });
        }

        /// <summary>
        /// 根据编码获取实体
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public IList<SysItemsDetail> GetItemList(string enCode)
        {
            string sql = "select d.* from sys_itemsdetail d inner join sys_items i on i.id = d.item_id "
                       + "where i.en_code = @enCode and d.enabled_mark = 1 and d.delete_mark = 0 "
                       + "order by d.sort_code asc";

            return MysqlHelper.GetList<SysItemsDetail, SysItems, SysItemsDetail>(sql,
                (detail, item) =>
                {
                    detail.item_owner = item;
                    return detail;
                }, new { enCode = enCode }, "delete_user");
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysItemsDetail GetForm(string keyValue)
        {
            string sql = "select * from sys_itemsdetail where id=@Id";
            return MysqlHelper.GetOne<SysItemsDetail>(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            string sql = "delete from sys_itemsdetail where id=@Id";
            MysqlHelper.Execute(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 更新/新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysItemsDetail model, string keyValue)
        {
            string sql;
            if (!string.IsNullOrEmpty(keyValue))
            {
                sql = "update sys_itemsdetail set item_id=@item_id, parent_id=@parent_id, "
                    + "item_code=@item_code, item_name=@item_name, simple_spelling=@simple_spelling, "
                    + "is_default=@is_default, sort_code=@sort_code, enabled_mark=@enabled_mark,  "
                    + "description=@description, last_modify_time=@last_modify_time, "
                    + "last_modify_user=@last_modify_user where id=@id";
                model.last_modify_time = DateTime.Now;
                model.last_modify_user = OperatorProvider.Provider.GetCurrent().UserId;
            }
            else
            {
                sql = "insert into sys_itemsdetail values (@id, @item_id, @parent_id, @item_code, "
                    + "@item_name, @simple_spelling, @is_default, @layers, @sort_code, @delete_mark, "
                    + "@enabled_mark, @description, @create_time, @create_user, @last_modify_time, "
                    + "@last_modify_user, @delete_time, @delete_user)";
                model.create_time = DateTime.Now;
                model.id = PublicFunction.GUID();
                model.create_user = OperatorProvider.Provider.GetCurrent().UserId;
            }

            MysqlHelper.Execute(sql, model);
        }
    }
}
