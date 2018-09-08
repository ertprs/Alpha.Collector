using MySql.Data.MySqlClient;
using System.Data;

namespace Alpha.Collector.Dao
{
    /// <summary>
    /// Dapper 数据库连接工厂
    /// </summary>
    public static class ConnectionFactory
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <returns></returns>
        public static IDbConnection CreateInstance(string connStr)
        {
            IDbConnection conn = new MySqlConnection(connStr);
            conn.Open();
            return conn;
        }
    }
}
