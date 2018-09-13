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
    /// 模块按钮App
    /// </summary>
    public class SysModuleButtonApp
    {
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public IList<SysModuleButton> GetList(string moduleId = "")
        {
            string sql = "select * from sys_modulebutton where 1=1 ";
            if (!string.IsNullOrEmpty(moduleId))
            {
                sql += "and module_id=@ModuleId ";
            }
            sql += "order by sort_code ";

            return MysqlHelper.GetList<SysModuleButton>(sql, new { ModuleId = moduleId });
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysModuleButton GetForm(string keyValue)
        {
            string sql = "select * from sys_modulebutton where id=@Id";
            return MysqlHelper.GetOne<SysModuleButton>(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            string sql = "select 1 from sys_modulebutton where parent_id=@Id";
            IList<SysModuleButton> list = MysqlHelper.GetList<SysModuleButton>(sql, new { Id = keyValue });
            if (list.Count > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                sql = "delete from sys_modulebutton where id=@Id";
                MysqlHelper.Execute(sql, new { Id = keyValue });
            }
        }

        /// <summary>
        /// 更新/新增实体
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysModuleButton model, string keyValue)
        {
            string sql;
            if (!string.IsNullOrEmpty(keyValue))
            {
                sql = "update sys_modulebutton set parent_id=@parent_id, parent_id=@parent_id, "
                    + "layers=@layers, en_code=@en_code, full_name=@full_name, "
                    + "icon=@icon, location=@location, js_event=@js_event, url_address=@url_address, "
                    + "split=@split, is_public=@is_public,allow_edit=@allow_edit, "
                    + "allow_delete=@allow_delete, sort_code=@sort_code, enabled_mark=@enabled_mark, "
                    + "description=@description, last_modify_time=@last_modify_time, "
                    + "last_modify_user=@last_modify_user where id=@id";
                model.last_modify_time = DateTime.Now;
                model.last_modify_user = OperatorProvider.Provider.GetCurrent().UserId;
            }
            else
            {
                sql = "insert into sys_modulebutton values (@id, @module_id, @parent_id, @layers, "
                    + "@en_code, @full_name, @icon, @location, @js_event, @url_address, @split, "
                    + "@is_public, @allow_edit, @allow_delete, @sort_code, @delete_mark, @enabled_mark, "
                    + "@description, @create_time, @create_user, @last_modify_time, "
                    + "@last_modify_user, @delete_time, @delete_user)";
                model.id = PublicFunction.GUID();
                model.create_time = DateTime.Now;
                model.create_user = OperatorProvider.Provider.GetCurrent().UserId;
            }

            MysqlHelper.Execute(sql, model);
        }

        /// <summary>
        /// 克隆按钮提交
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="ids"></param>
        public void SubmitCloneButton(string moduleId, string ids)
        {
            string[] arrayId = ids.Split(',');
            var data = this.GetList();
            IList<SysModuleButton> modelList = new List<SysModuleButton>();
            SysModuleButton model;
            IList<SysModuleButton> tmpList;
            foreach (string id in arrayId)
            {
                tmpList = data.Where(t => t.id == id).ToList();
                if (tmpList.Count == 0)
                {
                    continue;
                }

                model = tmpList[0];
                model.id = PublicFunction.GUID();
                model.module_id = moduleId;
                modelList.Add(model);
            }

            string sql = "insert into sys_modulebutton values (@id, @module_id, @parent_id, @layers, "
                       + "@en_code, @full_name, @icon, @location, @js_event, @url_address, @split, "
                       + "@is_public, @allow_edit, @allow_delete, @sort_code, @delete_mark, @enabled_mark, "
                       + "@description, @create_time, @create_user, @last_modify_time, "
                       + "@last_modify_user, @delete_time, @delete_user)";
            MysqlHelper.Execute(sql, modelList);
        }
    }
}
