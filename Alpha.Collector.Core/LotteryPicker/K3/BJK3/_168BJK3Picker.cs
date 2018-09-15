using Alpha.Collector.Model;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 168开奖网抓取北京快3
    /// </summary>
    internal class _168BJK3Picker : BasePicker, IPicker, IBJK3Picker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "https://api.api68.com/lotteryJSFastThree/getJSFastThreeList.do?date=&lotCode=10033";

        /// <summary>
        /// 执行抓取
        /// </summary>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                _168Picker picker = new _168Picker(URL, LotteryEnum.BJK3);
                return picker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.BJK3,
                    data_source = DataSourceEnum._168,
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
                return base.LotteryList.Contains(LotteryEnum.BJK3) && base.DataSourceList.Contains(DataSourceEnum._168);
            }
        }
    }
}
