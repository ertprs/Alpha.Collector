using Alpha.Collector.Core;
using Alpha.Collector.Dao;
using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Alpha.Collector.Web.Controllers
{
    /// <summary>
    /// 彩种
    /// </summary>
    public class LotteryController : BaseController
    {
        /// <summary>
        /// 彩种列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult List(Query<Lottery, LotteryParams> query)
        {
            string queryStr = "1=1 ";
            if (!string.IsNullOrEmpty(query.Params.LotteryCode))
            {
                queryStr += "and code = '" + query.Params.LotteryCode.ToUpper().Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(query.Params.State))
            {
                queryStr += "and status = " + query.Params.State + " ";
            }

            PagerInfo pagerInfo = new PagerInfo();
            pagerInfo.TableName = "lottery";
            pagerInfo.PKField = "id";
            pagerInfo.sidx = "id desc";
            pagerInfo.QueryString = queryStr;
            pagerInfo.page = query.__pageIndex;
            pagerInfo.rows = query.__pageSize;

            List<Lottery> resultList = MysqlHelper.GetListPagination<Lottery>(pagerInfo);
            query.ItemList = resultList;
            query.__pageIndex = query.__pageIndex;
            query.__pageSize = query.__pageSize;
            query.__totalRecord = pagerInfo.records;

            if (!Request.IsAjaxRequest())
            {
                ViewBag.LotteryList = (from l in base.LotteryList select new SelectListItem { Text = l.name, Value = l.code }).ToList();
            }

            return PageView(query);
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateStatus(string id, int status)
        {
            int result = LotteryApp.UpdateStatus(id.TrimEnd(','), status);
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.ResultCode = result;
            serviceResult.Message = result >= 0 ? "更新成功。重启抓取服务才能生效" : "更新失败！";
            return Json(serviceResult);
        }

        /// <summary>
        /// 采集详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PickDetail(string id, string lotteryCode)
        {
            List<LotteryPickDetail> detailList = LotteryApp.GetPickDetail(lotteryCode);
            List<LotteryPickDetail> newList = (from d in detailList
                                        let p = LotteryHelper.GetTypeList(lotteryCode)
                                        let p3 = LotteryHelper.GetPicker(lotteryCode, d.Code)
                                        let p2 = p == null ? new List<Type>() : p.Where(pp => pp.Name.Contains(d.Code)).ToList()
                                        select new LotteryPickDetail
                                        {
                                            Code = d.Code,
                                            TodayCount = d.TodayCount,
                                            TotalCount = d.TotalCount,
                                            PickerName = p2.Count == 0 ? "" : p2[0].Name,
                                            PickerValid = p3 == null ? false : p3.IsValid
                                        }).Where(p => !string.IsNullOrEmpty(p.PickerName)).ToList();
            return View(newList);
        }
    }
}