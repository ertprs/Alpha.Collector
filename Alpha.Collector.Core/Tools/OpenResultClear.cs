using Alpha.Collector.Dao;
using Alpha.Collector.Utils;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 开奖结果清除工具
    /// </summary>
    public class OpenResultClear
    {
        /// <summary>
        /// 清除开奖结果
        /// </summary>
        /// <param name="days">清除多少天之前的开奖结果</param>
        /// <returns></returns>
        public static int Clear(int days = 30)
        {
            DateTime time = DateTime.Now.AddDays(-30).Date;
            return OpenResultApp.Delete(time.ToTimestamp());
        }
    }
}
