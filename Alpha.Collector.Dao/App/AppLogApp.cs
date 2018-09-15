using System;
using Alpha.Collector.Model;

namespace Alpha.Collector.Dao
{
    /// <summary>
    /// 应用程序日志
    /// </summary>
    public class AppLogApp
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="appLog"></param>
        /// <returns></returns>
        public static int Insert(AppLog appLog)
        {
            string sql = "insert into app_log (create_time, create_timestamp, log_type, lottery_code, data_source, log_message)"
                       + "values(@create_time, @create_timestamp, @log_type, @lottery_code, @data_source, @log_message)";
            return MysqlHelper.Execute(sql, appLog);
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int Delete(long timestamp)
        {
            string sql = "detele from app_log where create_timestamp < {0}";
            sql = string.Format(sql, timestamp);
            return MysqlHelper.Execute(sql);
        }
    }
}
