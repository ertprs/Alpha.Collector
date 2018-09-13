using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 168开奖网抓取体彩排列3
    /// </summary>
    internal class _168TCPL3Picker : IPicker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "https://api.api68.com/QuanGuoCai/getLotteryInfoList.do?lotCode=10043";

        /// <summary>
        /// 执行抓取
        /// </summary>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                _168Picker<_168Data> picker = new _168Picker<_168Data>(URL, LotteryType.TCPL3);
                List<_168Data> dataList = picker.Pick();

                return (from o in dataList
                        select new OpenResult
                        {
                            create_time = DateTime.Now,
                            open_time = DateTime.Parse(o.preDrawTime),
                            lottery_code = LotteryType.TCPL3,
                            issue_number = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd") + o.preDrawIssue.Replace(DateTime.Now.ToString("yyyy"), "")),
                            open_data = o.preDrawCode,
                            data_source = DataSource._168
                        }).OrderBy(o => o.issue_number).ToList();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogType.ERROR,
                    lottery_code = LotteryType.TCPL3,
                    data_source = DataSource._168,
                    log_message = ex.ToString()
                };
                AlphaLogManager.Error(appLog);

                return new List<OpenResult>();
            }
        }
    }
}
