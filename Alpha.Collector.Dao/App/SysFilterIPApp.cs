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
    /// <summary>
    /// 过滤IP APP
    /// </summary>
    public class SysFilterIPApp
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysFilterIP> GetList(string keyword)
        {
            string sql = "select * from Sys_FilterIP where 1=1 ";
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += "and F_StartIP like @StartIP ";
            }
            sql += "order by delete_time desc";
            return MysqlHelper.GetList<SysFilterIP>(sql, new { StartIP = "%" + keyword + "%" }).ToList();
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysFilterIP GetForm(string keyValue)
        {
            string sql = "select * from Sys_FilterIP where id=@Id";
            return MysqlHelper.GetOne<SysFilterIP>(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            string sql = "delete from Sys_FilterIP where id=@Id";
            MysqlHelper.Execute(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 更新/新增实体
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysFilterIP model, string keyValue)
        {
            string sql;
            if (!string.IsNullOrEmpty(keyValue))
            {
                sql = "update Sys_FilterIP set F_Type=@F_Type, F_StartIP=@F_StartIP, F_EndIP=@F_EndIP, "
                    + "sort_time=@sort_time, enabled_mark=@enabled_mark, description=@description, "
                    + "last_modify_time=@last_modify_time, last_modify_user=@last_modify_user";
                model.last_modify_time = DateTime.Now;
                model.last_modify_user = OperatorProvider.Provider.GetCurrent().UserId;
            }
            else
            {
                sql = "insert intp Sys_FilterIP values (@id, F_Type, F_StartIP, F_EndIP, sort_time, "
                    + "delete_mark, enabled_mark, description, create_time, create_user)";
                model.id = PublicFunction.GUID();
                model.create_time = DateTime.Now;
                model.create_user = OperatorProvider.Provider.GetCurrent().UserId;
            }

            MysqlHelper.Execute(sql, model);
        }
    }
}
