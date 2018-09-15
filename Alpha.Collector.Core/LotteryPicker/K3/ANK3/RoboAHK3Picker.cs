using Alpha.Collector.Model;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 乐博安徽快3采集器
    /// </summary>
    public class RoboAHK3Picker : BasePicker, IPicker, IAHK3Picker
    {
        /// <summary>
        /// 执行抓取
        /// </summary>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                RoboPicker roboPicker = new RoboPicker(LotteryEnum.AHK3);
                return roboPicker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.AHK3,
                    data_source = DataSourceEnum.Robo,
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
                return base.LotteryList.Contains(LotteryEnum.AHK3) && base.DataSourceList.Contains(DataSourceEnum.Robo);
            }
        }
    }
}
