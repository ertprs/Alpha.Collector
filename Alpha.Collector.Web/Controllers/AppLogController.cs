using Alpha.Collector.Dao;
using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Alpha.Collector.Web.Controllers
{
    /// <summary>
    /// 日志
    /// </summary>
    public class AppLogController : BaseController
    {
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult List(Query<AppLog, AppLogParams> query)
        {
            if (query.Params.StartTime == DateTime.MinValue)
            {
                query.Params.StartTime = DateTime.Now.Date;
            }

            if (query.Params.EndTime == DateTime.MinValue)
            {
                query.Params.EndTime = DateTime.Now.AddDays(1).AddMilliseconds(-1); ;
            }

            string queryStr = "1=1 ";
            if (!string.IsNullOrEmpty(query.Params.LotteryCode))
            {
                queryStr += "and lottery_code = '" + query.Params.LotteryCode.ToUpper().Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(query.Params.DataSource))
            {
                queryStr += "and data_source like '%" + query.Params.DataSource.Trim() + "%' ";
            }

            if (query.Params.StartTime != DateTime.MinValue)
            {

                queryStr += "and create_timestamp >= " + query.Params.StartTime.ToTimestamp() + " ";
            }

            if (query.Params.EndTime != DateTime.MinValue)
            {
                queryStr += "and create_timestamp <= " + query.Params.EndTime.ToTimestamp() + " ";
            }

            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.TableName = "app_log";
            pagerInfo.PKField = "id";
            pagerInfo.sidx = "id desc";
            pagerInfo.QueryString = queryStr;
            pagerInfo.page = query.__pageIndex;
            pagerInfo.rows = query.__pageSize;

            List<AppLog> resultList = MysqlHelper.GetListPagination<AppLog>(pagerInfo);
            query.ItemList = resultList;
            query.__pageIndex = query.__pageIndex;
            query.__pageSize = query.__pageSize;
            query.__totalRecord = pagerInfo.records;

            if (!Request.IsAjaxRequest())
            {
                ViewBag.LotteryList = (from l in base.LotteryList select new SelectListItem { Text = l.name, Value = l.code }).ToList();
                ViewBag.DataSourceList = (from l in base.DataSourceList select new SelectListItem { Text = l.name, Value = l.code }).ToList();
            }

            return PageView(query);
        }

        /// <summary>
        /// 显示详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult AppLogDetail(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}