using Alpha.Collector.Model;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 快彩在线抓取重庆时时彩
    /// </summary>
    internal class KCCQSSCPicker : BasePicker, IPicker, ICQSSCPicker
    {
        private KCPicker _kcPicker;

        public KCCQSSCPicker()
        {
            this._kcPicker = new KCPicker(LotteryEnum.CQSSC);
        }

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
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
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.CQSSC,
                    data_source = DataSourceEnum.KC,
                    log_message = ex.ToString()
                };
                AlphaLogManager.Error(appLog);

                return new List<OpenResult>();
            }
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get
            {
                return base.LotteryList.Contains(LotteryEnum.CQSSC) && base.DataSourceList.Contains(DataSourceEnum.KC);
            }
        }
    }
}
