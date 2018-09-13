using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Dao
{
    public class SysLogApp
    {
        /// <summary>
     /// 获取日志列表
     /// </summary>
     /// <param name="pagerInfo"></param>
     /// <param name="queryJson"></param>
     /// <returns></returns>
        public List<SysLog> GetList(PagerInfo pagerInfo, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string sql = "1=1 ";

            string keyword = string.Empty;
            string timeType = string.Empty;
            string startTime = DateTime.Now.ToString("yyyy-MM-dd");
            string endTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            if (!queryParam["keyword"].IsEmpty())
            {
                keyword = queryParam["keyword"].ToString();
                sql += "and account like '%" + keyword + "%' ";
            }

            if (!queryParam["timeType"].IsEmpty())
            {
                timeType = queryParam["timeType"].ToString();
                switch (timeType)
                {
                    case "1":
                        break;
                    case "2":
                        startTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                        break;
                    case "3":
                        startTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                        break;
                    case "4":
                        startTime = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
                        break;
                    default:
                        break;
                }
                sql += "and create_time >= '" + startTime + "' and create_time <= '" + endTime + "' ";
            }
            pagerInfo.TableName = "sys_log";
            pagerInfo.QueryString = sql;
            return MysqlHelper.GetListPagination<SysLog>(pagerInfo);
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="keepTime"></param>
        public void RemoveLog(string keepTime)
        {
            DateTime operateTime = DateTime.Now;
            if (keepTime == "7")            //保留近一周
            {
                operateTime = DateTime.Now.AddDays(-7);
            }
            else if (keepTime == "1")       //保留近一个月
            {
                operateTime = DateTime.Now.AddMonths(-1);
            }
            else if (keepTime == "3")       //保留近三个月
            {
                operateTime = DateTime.Now.AddMonths(-3);
            }

            string sql = "delete from sys_log where create_time <= @Date";
            MysqlHelper.Execute(sql, new { Date = operateTime });
        }

        /// <summary>
        /// 写日志到数据库
        /// </summary>
        /// <param name="result"></param>
        /// <param name="resultLog"></param>
        public void WriteDbLog(bool result, string resultLog)
        {
            SysLog log = new SysLog();
            log.id = PublicFunction.GUID();
            log.create_time = DateTime.Now;
            log.account = OperatorProvider.Provider.GetCurrent().UserCode;
            log.nick_name = OperatorProvider.Provider.GetCurrent().UserName;
            log.ip = NetHelper.Ip;
            log.ip_address = NetHelper.GetLocation(log.ip);
            log.result = result;
            log.description = resultLog;
            var loginUser = OperatorProvider.Provider.GetCurrent();
            log.create_user = loginUser == null ? "" : loginUser.UserId;

            string sql = "insert into sys_log values (@id, @create_time, @account, @nick_name, @type, "
                       + "@ip, @ip_address, @module_id, @module_name, @result, @description, @create_user)";
            MysqlHelper.Execute(sql, log);
        }

        /// <summary>
        /// 写日志到数据库
        /// </summary>
        /// <param name="log"></param>
        public void WriteDbLog(SysLog log)
        {
            log.create_time = DateTime.Now;
            log.id = PublicFunction.GUID();
            log.ip = NetHelper.Ip;
            log.ip_address = NetHelper.GetLocation(log.ip);

            var loginUser = OperatorProvider.Provider.GetCurrent();
            log.create_user = loginUser == null ? "": loginUser.UserId;

            string sql = "insert into sys_log values (@id, @create_time, @account, @nick_name, @type, "
                       + "@ip, @ip_address, @module_id, @module_name, @result, @description, @create_user)";
            MysqlHelper.Execute(sql, log);
        }
    }
}

