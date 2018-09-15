using Alpha.Collector.Model;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 168开奖网抓取PC蛋蛋
    /// </summary>
    internal class _168PCDDPicker : BasePicker, IPicker, IPCDDPicker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "https://api.api68.com/LuckTwenty/getPcLucky28List.do?date=";

        /// <summary>
        /// 执行抓取
        /// </summary>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                _168Picker picker = new _168Picker(URL, LotteryEnum.PCDD);
                return picker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.PCDD,
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
                return base.LotteryList.Contains(LotteryEnum.PCDD) && base.DataSourceList.Contains(DataSourceEnum._168);
            }
        }
    }
}
