namespace Alpha.Collector.Model
{
    /// <summary>
    /// 日志记录位置
    /// </summary>
    public static class LogPositionEnum
    {
        /// <summary>
        /// 日志
        /// </summary>
        public const string FILE = "FILE";

        /// <summary>
        /// Mysql数据库
        /// </summary>
        public const string MYSQL = "MYSQL";

        /// <summary>
        /// Redis数据库
        /// </summary>
        public const string REDIS = "REDIS";
    }
}
