using Alpha.Collector.Model;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 乐博湖北快3采集器
    /// </summary>
    internal class RoboHuBK3Picker : BasePicker, IPicker, IHuBK3Picker
    {
        /// <summary>
        /// 执行抓取
        /// </summary>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                RoboPicker roboPicker = new RoboPicker(LotteryEnum.HuBK3);
                return roboPicker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.HuBK3,
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
                return base.LotteryList.Contains(LotteryEnum.HuBK3) && base.DataSourceList.Contains(DataSourceEnum.Robo);
            }
        }
    }
}
