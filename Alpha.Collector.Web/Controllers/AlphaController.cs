using Alpha.Collector.Dao;
using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace Alpha.Collector.Web.Controllers
{
    /// <summary>
    /// Alpha API
    /// </summary>
    //[RoutePrefix("alpha")]
    public class AlphaController : Controller
    {
        /// <summary>
        /// 取某一个彩种的最新的20期开奖结果
        /// </summary>
        /// <param name="lottery"></param>
        /// <returns></returns>
        //[HttpGet, HttpPost, Route("getList")]
        public ActionResult GetOpenResultList(string lottery)
        {
            List<OpenResult> resultList = OpenResultApp.GetNewestResult(lottery);
            var list = (from r in resultList
                        select new
                        {
                            issue_no = r.issue_number,
                            open_time = r.open_time,
                            open_data = r.open_data
                        }).ToList();

            return Content(list.ToJson());
        }
    }
}
