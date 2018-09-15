using Alpha.Collector.Model;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 168开奖网抓取天津时时彩
    /// </summary>
    internal class _168TJSSCPicker : BasePicker, IPicker, ITJSSCPicker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "https://api.api68.com/CQShiCai/getBaseCQShiCaiList.do?lotCode=10003&t={$random}";

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                _168Picker picker = new _168Picker(URL, LotteryEnum.TJSSC);
                return picker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.TJSSC,
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
                return base.LotteryList.Contains(LotteryEnum.TJSSC) && base.DataSourceList.Contains(DataSourceEnum._168);
            }
        }
    }
}
