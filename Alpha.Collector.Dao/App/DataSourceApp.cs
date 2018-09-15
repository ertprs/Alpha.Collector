using Alpha.Collector.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using Alpha.Collector.Utils;

namespace Alpha.Collector.Dao
{
    /// <summary>
    /// 数据源App
    /// </summary>
    public class DataSourceApp
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int Insert(List<DataSource> list)
        {
            string sql = "insert into data_source (create_time, create_timestamp, name, code) values (@create_time, @create_timestamp, @name, @code)";
            return MysqlHelper.Execute(sql, list);
        }

        /// <summary>
        /// 获取所有彩种
        /// </summary>
        /// <returns></returns>
        public static List<DataSource> GetList()
        {
            string sql = "select * from data_source";
            return MysqlHelper.GetList<DataSource>(sql).ToList();
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int UpdateStatus(int id, int status)
        {
            string sql = "update data_source set status = {0} where id = {1}";
            sql = string.Format(sql, status, id);
            return MysqlHelper.Execute(sql);
        }

        /// <summary>
        /// 获取采集数量
        /// </summary>
        /// <param name="codeList"></param>
        /// <returns></returns>
        public static List<PickCount> GetPickCount(string codeList)
        {
            string sql = $@"select c.code Code, ifnull(b.TodayCount, 0) TodayCount, ifnull(a.TotalCount, 0) TotalCount from data_source c
                            left join (select data_source, count(1) TotalCount from open_result a where data_source in ({codeList}) group by data_source) a on a.data_source = c.code 
                            left join (select data_source, count(1) TodayCount from open_result where data_source in ({codeList}) and create_timestamp >= {DateTime.Now.Date.ToTimestamp()}  group by data_source) b 
                            on b.data_source = c.code where c.code in ({codeList})";
            return MysqlHelper.GetList<PickCount>(sql).ToList();
        }

        /// <summary>
        /// 获取数据源采集详情
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public static List<DataSourcePickDetail> GetPickDetail(string dataSource)
        {
            string sql = $@"select c.code Code, ifnull(b.TodayCount, 0) TodayCount, ifnull(a.TotalCount, 0) TotalCount from lottery c
                            left join (select lottery_code, count(1) TotalCount from open_result where data_source = '{dataSource}' group by lottery_code) a on a.lottery_code = c.code 
                            left join (select lottery_code, count(1) TodayCount from open_result where data_source = '{dataSource}' and create_timestamp >= {DateTime.Now.Date.ToTimestamp()}  group by lottery_code) b 
                            on b.lottery_code = c.code";
            return MysqlHelper.GetList<DataSourcePickDetail>(sql).ToList();
        }
    }
}
