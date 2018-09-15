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
    /// 开奖结果
    /// </summary>
    public class OpenResultController : BaseController
    {
        /// <summary>
        /// 开奖结果列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult List(Query<OpenResult, OpenResultParams> query)
        {
            if (query.Params.StartOpenTime == DateTime.MinValue)
            {
                query.Params.StartOpenTime = DateTime.Now.Date;
            }

            if (query.Params.EndOpenTime == DateTime.MinValue)
            {
                query.Params.EndOpenTime = DateTime.Now.AddDays(1).AddMilliseconds(-1); ;
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

            if (query.Params.StartOpenTime != DateTime.MinValue)
            {

                queryStr += "and open_timestamp >= " + query.Params.StartOpenTime.ToTimestamp() + " ";
            }

            if (query.Params.EndOpenTime != DateTime.MinValue)
            {
                queryStr += "and open_timestamp <= " + query.Params.EndOpenTime.ToTimestamp() + " ";
            }

            if (!string.IsNullOrEmpty(query.Params.IssueNumber))
            {
                queryStr += "and issue_number= " + query.Params.IssueNumber + " ";
            }

            if (!string.IsNullOrEmpty(query.Params.IsLegal))
            {
                queryStr += "and is_legal = " + query.Params.IsLegal + " ";
            }


            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.TableName = "open_result";
            pagerInfo.PKField = "id";
            pagerInfo.sidx = "id desc";
            pagerInfo.QueryString = queryStr;
            pagerInfo.page = query.__pageIndex;
            pagerInfo.rows = query.__pageSize;

            List<OpenResult> resultList = MysqlHelper.GetListPagination<OpenResult>(pagerInfo);
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
    }
}