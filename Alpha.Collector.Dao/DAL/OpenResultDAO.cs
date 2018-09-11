using Alpha.Collector.Model;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Dao
{
    /// <summary>
    /// OpenResult表操作类
    /// </summary>
    public static class OpenResultDAO
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
            return MySqlHelper.Execute(sql, resultList.Where(o => !Exists(o.issue_number, o.lottery_code)).OrderBy(o => o.issue_number));
        }

        /// <summary>
        /// 判断当前期在数据库是否已经存在
        /// </summary>
        /// <param name="issueNo"></param>
        /// <returns></returns>
        private static bool Exists(long issueNo, string lotteryCode)
        {
            string sql = "select * from open_result where issue_number = {0} and lottery_code = '{1}'";
            sql = string.Format(sql, issueNo, lotteryCode);
            OpenResult o = MySqlHelper.GetOne<OpenResult>(sql);
            return o != null && o.id > 0;
        }
    }
}
