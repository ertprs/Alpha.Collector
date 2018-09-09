using Alpha.Collector.Model;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 快彩在线抓取重庆时时彩
    /// </summary>
    internal class KCCQSSCCollector : ICQSSCCollector
    {
        private KCPicker _kcPicker;

        public KCCQSSCCollector()
        {
            this._kcPicker = new KCPicker(1000);
        }

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> ICQSSCCollector.Run()
        {
            try
            {
                return this._kcPicker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = "Error",
                    lottery_code = LotteryCodeEnum.CQSSC,
                    data_source = DataSourceEnum.KCZX,
                    log_message = ex.ToString()
                };
                AlphaLogManager.Error(appLog);

                return new List<OpenResult>();
            }
        }
    }
}
