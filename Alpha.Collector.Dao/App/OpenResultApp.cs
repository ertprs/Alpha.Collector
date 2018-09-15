using Alpha.Collector.Model;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Dao
{
    /// <summary>
    /// OpenResult表操作类
    /// </summary>
    public static class OpenResultApp
    {
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="resultList"></param>
        /// <returns></returns>
        public static int Insert(List<OpenResult> resultList)
        {
            if (resultList.Count == 0)
            {
                return 1;
            }

            string sql = "insert into open_result (create_time, create_timestamp, open_time, open_timestamp, lottery_code, issue_number, open_data, data_source, is_legal) "
                       + "values (@create_time, @create_timestamp, @open_time, @open_timestamp, @lottery_code, @issue_number, @open_data, @data_source, @is_legal)";
            return MysqlHelper.Execute(sql, resultList.Where(o => !Exists(o.issue_number, o.lottery_code, o.data_source)).OrderBy(o => o.issue_number));
        }

        /// <summary>
        /// 取某一个彩种最新的开奖结果
        /// </summary>
        /// <param name="lotteryType"></param>
        /// <returns></returns>
        public static List<OpenResult> GetNewestResult(string lotteryType)
        {
            string sql = $"select issue_number, open_time, open_data from open_result where lottery_code = '{lotteryType}' group by issue_number order by issue_number desc limit 20";
            return MysqlHelper.GetList<OpenResult>(sql).ToList();
        }

        /// <summary>
        /// 判断当前期在数据库是否已经存在
        /// </summary>
        /// <param name="issueNo"></param>
        /// <returns></returns>
        private static bool Exists(long issueNo, string lotteryCode, string dataSource)
        {
            string sql = $"select * from open_result where issue_number = {issueNo} and lottery_code = '{lotteryCode}' and data_source = '{dataSource}'";
            OpenResult o = MysqlHelper.GetOne<OpenResult>(sql);
            return o != null && o.id > 0;
        }

        /// <summary>
        /// 清除指定时间之前的开奖结果
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static int Delete(long timestamp)
        {
            string sql = "detele from open_result where create_timestamp < {0}";
            sql = string.Format(sql, timestamp);
            return MysqlHelper.Execute(sql);
        }
    }
}
