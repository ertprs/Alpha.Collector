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
    }
}
