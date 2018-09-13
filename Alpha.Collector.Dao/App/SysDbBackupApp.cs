using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Dao
{
    public class SysDbBackupApp
    {

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public List<SysDbBackup> GetList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string sql = "select * from sys_dbbackup where 1=1 ";
            string keyword = string.Empty;
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "DbName":
                        sql += "and db_name like @DbName ";
                        break;
                    case "FileName":
                        sql += "and file_name like @DbName ";
                        break;
                }
            }
            sql += "order by backup_time desc";
            return MysqlHelper.GetList<SysDbBackup>(sql, new { DbName = keyword }).ToList();
        }

        /// <summary>
        /// 获取指定实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysDbBackup GetForm(string keyValue)
        {
            string sql = "select * from sys_dbbackup where id=@Id";
            return MysqlHelper.GetOne<SysDbBackup>(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            string sql = "delete from sys_dbbackup where id=@Id";
            MysqlHelper.Execute(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="model"></param>
        public void SubmitForm(SysDbBackup model)
        {
            model.id = PublicFunction.GUID();
            model.enabled_mark = true;
            model.backup_time = DateTime.Now;

            string sql = "insert into sys_dbbackup values (@id, @backup_type, @db_name, @file_name, "
                       + "@file_size, @file_path, @backup_time, @sort_time, @delete_mark, @enabled_mark, "
                       + "@description, @create_time, @create_user, @last_modify_time, @last_modify_user, "
                       + "@delete_time, @delete_user)";
            MysqlHelper.Execute(sql, model);
        }
    }
}
