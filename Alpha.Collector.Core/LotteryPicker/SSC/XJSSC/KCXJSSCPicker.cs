using Alpha.Collector.Model;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 快彩在线抓取新疆时时彩
    /// </summary>
    internal class KCXJSSCPicker : BasePicker, IPicker, IXJSSCPicker
    {
        private KCPicker _kcPicker;

        public KCXJSSCPicker()
        {
            this._kcPicker = new KCPicker(LotteryEnum.XJSSC);
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
                    lottery_code = LotteryEnum.XJSSC,
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
                return base.LotteryList.Contains(LotteryEnum.XJSSC) && base.DataSourceList.Contains(DataSourceEnum.KC);
            }
        }
    }
}
