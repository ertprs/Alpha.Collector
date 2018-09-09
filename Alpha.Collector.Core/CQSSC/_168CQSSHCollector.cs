using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 168开奖网抓取重庆时时彩
    /// </summary>
    internal class _168CQSSHCollector : ICQSSCCollector
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
                _168Picker<_168CQSSCData> picker = new _168Picker<_168CQSSCData>(URL, LotteryCodeEnum.CQSSC);
                List<_168CQSSCData> dataList = picker.Pick();

                return (from o in dataList
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
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = "Error",
                    lottery_code = LotteryCodeEnum.CQSSC,
                    data_source = DataSourceEnum._168,
                    log_message = ex.ToString()
                };
                AlphaLogManager.Error(appLog);

                return new List<OpenResult>();
            }
        }
    }
}
