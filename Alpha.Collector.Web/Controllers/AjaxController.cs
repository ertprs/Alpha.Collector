using Alpha.Collector.Core;
using Alpha.Collector.Dao;
using Alpha.Collector.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Alpha.Collector.Web.Controllers
{
    /// <summary>
    /// Ajax控制器
    /// </summary>
    public class AjaxController : BaseController
    {
        /// <summary>
        /// 获取彩种的采集情况
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetLotteryPickCount(string codeList)
        {
            codeList = codeList.TrimEnd(',').Split(',').ToList().Aggregate(string.Empty, (c, r) => c + "'" + r + "',").TrimEnd(',');
            List<PickCount> list = LotteryApp.GetPickCount(codeList);
            List<PickCount> newList = (from p in list
                                       let pl = LotteryHelper.GetPickerList(p.Code)
                                       select new PickCount
                                       {
                                           Code = p.Code,
                                           TodayCount = p.TodayCount,
                                           TotalCount = p.TotalCount,
                                           TotalPickerCount = pl.Count,
                                           ValidPickerCount = pl.Count(pp => pp.IsValid)
                                       }).ToList();

            return Json(newList);
        }

        /// <summary>
        /// 获取数据源的采集情况
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetDataSourcePickCount(string codeList)
        {
            codeList = codeList.TrimEnd(',').Split(',').ToList().Aggregate(string.Empty, (c, r) => c + "'" + r + "',").TrimEnd(',');
            List<PickCount> list = DataSourceApp.GetPickCount(codeList);

            return Json(list);
        }
    }
}