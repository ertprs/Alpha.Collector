using Alpha.Collector.Model;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 快彩在线抓取广东11选5
    /// </summary>
    internal class KCGD11X5Picker : BasePicker, IPicker, IGD11X5Picker
    {
        private KCPicker _kcPicker;

        public KCGD11X5Picker()
        {
            this._kcPicker = new KCPicker(LotteryEnum.GD11X5);
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
                    lottery_code = LotteryEnum.GD11X5,
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
                return base.LotteryList.Contains(LotteryEnum.GD11X5) && base.DataSourceList.Contains(DataSourceEnum.KC);
            }
        }
    }
}
