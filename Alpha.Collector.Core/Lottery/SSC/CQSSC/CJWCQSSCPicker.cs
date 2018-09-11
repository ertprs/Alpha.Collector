using System.Collections.Generic;
using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 彩经网采集重庆时时彩
    /// </summary>
    internal class CJWCQSSCPicker : IPicker
    {
        /// <summary>
        /// 抓取地址
        /// </summary>
        private const string URL = "https://shishicai.cjcp.com.cn/chongqing/kaijiang/";

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                CJWSSCPicker picker = new CJWSSCPicker(LotteryType.CQSSC, URL);
                return picker.Pick();
            }
            catch(Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogType.ERROR,
                    lottery_code = LotteryType.CQSSC,
                    data_source = DataSource.CJW,
                    log_message = ex.ToString()
                };
                AlphaLogManager.Error(appLog);

                return new List<OpenResult>();
            }
        }
    }
}
