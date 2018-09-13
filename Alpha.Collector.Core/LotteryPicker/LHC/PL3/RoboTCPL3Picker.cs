using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 乐博体彩排列3采集器
    /// </summary>
    public class RoboTCPL3Picker : IPicker
    {
        /// <summary>
        /// 执行抓取
        /// </summary>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                RoboPicker roboPicker = new RoboPicker(LotteryType.TCPL3);
                List<OpenResult> dataList = roboPicker.Pick();
                return (from o in dataList
                        select new OpenResult
                        {
                            create_time = o.create_time,
                            open_time = o.open_time,
                            lottery_code = LotteryType.TCPL3,
                            issue_number = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd") + o.issue_number.ToString().Replace(DateTime.Now.ToString("yyyy"), "")),
                            open_data = o.open_data,
                            data_source = o.data_source
                        }).OrderBy(o => o.issue_number).ToList();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogType.ERROR,
                    lottery_code = LotteryType.TCPL3,
                    data_source = DataSource.ROBO,
                    log_message = ex.ToString()
                };
                AlphaLogManager.Error(appLog);

                return new List<OpenResult>();
            }
        }
    }
}
