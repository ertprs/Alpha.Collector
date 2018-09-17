using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Dao
{
    /// <summary>
    /// 彩种App
    /// </summary>
    public class LotteryApp
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int Insert(List<Lottery> list)
        {
            string sql = "insert into lottery (create_time, create_timestamp, name, code) values (@create_time, @create_timestamp, @name, @code)";
            return MysqlHelper.Execute(sql, list.Where(l => !Exist(l.code)));
        }

        private static bool Exist(string lotteryCode)
        {
            string sql = $"select * from lottery where code = '{lotteryCode}'";
            return MysqlHelper.GetOne<OpenResult>(sql) != null;
        }

        /// <summary>
        /// 获取所有彩种
        /// </summary>
        /// <returns></returns>
        public static List<Lottery> GetList()
        {
            string sql = "select * from lottery";
            return MysqlHelper.GetList<Lottery>(sql).ToList();
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int UpdateStatus(string id, int status)
        {
            string sql = $"update lottery set status = {status}, update_time='{DateTime.Now}', update_timestamp={DateTime.Now.ToTimestamp()} where id in ({id})";
            return MysqlHelper.Execute(sql);
        }

        /// <summary>
        /// 获取采集数量
        /// </summary>
        /// <param name="codeList"></param>
        /// <returns></returns>
        public static List<PickCount> GetPickCount(string codeList)
        {
            string sql = $@"select c.code Code, ifnull(b.TodayCount, 0) TodayCount, ifnull(a.TotalCount, 0) TotalCount from lottery c
                            left join (select lottery_code, count(1) TotalCount from open_result a where lottery_code in ({codeList}) group by lottery_code) a on a.lottery_code = c.code 
                            left join (select lottery_code, count(1) TodayCount from open_result where lottery_code in ({codeList}) and create_timestamp >= {DateTime.Now.Date.ToTimestamp()}  group by lottery_code) b 
                            on b.lottery_code = c.code where c.code in ({codeList})";
            return MysqlHelper.GetList<PickCount>(sql).ToList();
        }

        /// <summary>
        /// 获取彩种采集详情
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <returns></returns>
        public static List<LotteryPickDetail> GetPickDetail(string lotteryCode)
        {
            string sql = $@"select c.code Code, ifnull(b.TodayCount, 0) TodayCount, ifnull(a.TotalCount, 0) TotalCount from data_source c
                            left join (select data_source, count(1) TotalCount from open_result where lottery_code = '{lotteryCode}' group by data_source) a on a.data_source = c.code 
                            left join (select data_source, count(1) TodayCount from open_result where lottery_code = '{lotteryCode}' and create_timestamp >= {DateTime.Now.Date.ToTimestamp()}  group by data_source) b 
                            on b.data_source = c.code";
            return MysqlHelper.GetList<LotteryPickDetail>(sql).ToList();
        }
    }
}
