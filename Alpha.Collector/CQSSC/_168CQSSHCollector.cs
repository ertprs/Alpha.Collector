using Alpha.Collector.Model._168;
using Alpha.Collector.Model.CQSSC;
using Alpha.Collector.Model.DataBase;
using Alpha.Collector.Model.Enums;
using Alpha.Collector.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.CQSSC
{
    /// <summary>
    /// 168开奖网抓取重庆时时彩
    /// </summary>
    public class _168CQSSHCollector : ICQSSCCollector
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "https://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?date=&lotCode=10002";

        /// <summary>
        /// 执行
        /// </summary>
        List<OpenResult> ICQSSCCollector.Run()
        {
            try
            {
                string json = HttpHelper.HttpGet(URL);
                _168Response<_168CQSSCData> response = JsonHelper.JsonToEntity<_168Response<_168CQSSCData>>(json, null);
                if (response.errorCode != 0)
                {
                    return new List<OpenResult>();
                }

                if (response.result.data == null || response.result.data.Count == 0)
                {
                    return new List<OpenResult>();
                }

                return (from o in response.result.data
                        select new OpenResult
                        {
                            create_time = DateTime.Now,
                            open_time = DateTime.Parse(o.preDrawTime),
                            lottery_code = LotteryCodeEnum.CQSSC,
                            issue_number = Convert.ToInt64(o.preDrawIssue),
                            open_data = o.preDrawCode,
                            data_source = DataSourceEnum._168
                        }).OrderBy(o => o.issue_number).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
