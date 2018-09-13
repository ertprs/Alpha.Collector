using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 必发抓取北京赛车
    /// </summary>
    internal class BiFaBJKCPicker : IPicker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "https://www.bf668.com/static//data/2018091250HistoryLottery.json?_={0}";

        /// <summary>
        /// 执行抓取
        /// </summary>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                long timeStamp = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                BiFaPicker picker = new BiFaPicker(string.Format(URL, timeStamp), LotteryType.BJKC);
                List<OpenResult> dataList = picker.DoPick();
                return dataList;
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogType.ERROR,
                    lottery_code = LotteryType.BJKC,
                    data_source = DataSource.BiFa,
                    log_message = ex.ToString()
                };
                AlphaLogManager.Error(appLog);

                return new List<OpenResult>();
            }
        }
    }
}
