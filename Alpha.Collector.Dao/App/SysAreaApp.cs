using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Dao
{
    /// <summary>
    /// 区域App
    /// </summary>
    public class SysAreaApp
    {

        /// <summary>
        /// 获取所有区域
        /// </summary>
        /// <returns></returns>
        public IList<SysArea> GetList()
        {
            string sql = "select * from sys_area where 1=1";
            return MysqlHelper.GetList<SysArea>(sql);
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysArea GetEntity(string keyValue)
        {
            string sql = "select * from sys_area where id=@Id";
            return MysqlHelper.GetOne<SysArea>(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteEntity(string keyValue)
        {
            string sql = "select 1 from sys_area where parent_id=@Id";
            IList<SysArea> list = MysqlHelper.GetList<SysArea>(sql, new { Id = keyValue });
            if (list.Count > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                sql = "delete from sys_area where id=@Id";
                MysqlHelper.Execute(sql, new { Id = keyValue });
            }
        }

        /// <summary>
        /// 提价表单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysArea model, string keyValue)
        {
            string sql;
            if (!string.IsNullOrEmpty(keyValue))
            {
                sql = "update sys_area set parent_id=@parent_id, layers=@layers, en_code=@en_code, "
                    + "full_name=@full_name, simple_spelling=@simple_spelling, sort_code=@sort_code, "
                    + "description=@description, last_modify_time=@last_modify_time, "
                    + "last_modify_user=@last_modify_user where id=@id";
                model.last_modify_time = DateTime.Now;
                model.last_modify_user = OperatorProvider.Provider.GetCurrent().UserId;
            }
            else
            {
                sql = "insert into sys_area values (@id, @parent_id, @layers, @en_code, @full_name, "
                    + "@simple_spelling, @sort_code, @delete_mark, @enabled_mark, @description, "
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
