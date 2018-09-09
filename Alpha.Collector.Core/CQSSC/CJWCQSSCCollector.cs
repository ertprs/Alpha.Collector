using System.Collections.Generic;
using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 财经网采集重庆时时彩
    /// </summary>
    internal class CJWCQSSCCollector : ICQSSCCollector
    {
        /// <summary>
        /// 抓取地址
        /// </summary>
        private const string URL = "https://shishicai.cjcp.com.cn/chongqing/kaijiang/";

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> ICQSSCCollector.Run()
        {
            try
            {
                CJWPicker picker = new CJWPicker(LotteryCodeEnum.CQSSC, URL);
                return picker.Pick();
            }
            catch(Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = "Error",
                    lottery_code = LotteryCodeEnum.CQSSC,
                    data_source = DataSourceEnum.CJW,
                    log_message = ex.ToString()
                };
                AlphaLogManager.Error(appLog);

                return new List<OpenResult>();
            }
        }
    }
}
