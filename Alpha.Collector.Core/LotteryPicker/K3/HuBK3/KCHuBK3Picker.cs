using Alpha.Collector.Model;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 快彩在线采集湖北快三
    /// </summary>
    internal class KCHuBK3Picker : BasePicker, IPicker, IHuBK3Picker
    {
        private KCPicker _kcPicker;

        public KCHuBK3Picker()
        {
            this._kcPicker = new KCPicker(LotteryEnum.HuBK3);
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
                    lottery_code = LotteryEnum.HuBK3,
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
                return base.LotteryList.Contains(LotteryEnum.HuBK3) && base.DataSourceList.Contains(DataSourceEnum.KC);
            }
        }
    }
}
