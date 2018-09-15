using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 乐博PC蛋蛋采集器
    /// </summary>
    public class RoboPCDDPicker : BasePicker, IPicker, IPCDDPicker
    {
        /// <summary>
        /// 执行抓取
        /// </summary>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                RoboPicker roboPicker = new RoboPicker(LotteryEnum.BJKL8);
                List<OpenResult> list = roboPicker.Pick();
                return (from r in list
                        let arr = r.open_data.Split(',')
                        select new OpenResult
                        {
                            create_time = r.create_time,
                            open_time = r.open_time,
                            lottery_code = LotteryEnum.PCDD,
                            issue_number = r.issue_number,
                            open_data = ((Convert.ToInt32(arr[0]) + Convert.ToInt32(arr[1]) + Convert.ToInt32(arr[2]) + Convert.ToInt32(arr[3]) + Convert.ToInt32(arr[4]) + Convert.ToInt32(arr[5])) % 10) + "," +
                                        ((Convert.ToInt32(arr[6]) + Convert.ToInt32(arr[7]) + Convert.ToInt32(arr[8]) + Convert.ToInt32(arr[9]) + Convert.ToInt32(arr[10]) + Convert.ToInt32(arr[11])) % 10) + "," +
                                        ((Convert.ToInt32(arr[12]) + Convert.ToInt32(arr[13]) + Convert.ToInt32(arr[14]) + Convert.ToInt32(arr[15]) + Convert.ToInt32(arr[16]) + Convert.ToInt32(arr[17])) % 10),
                            data_source = r.data_source
                        }).ToList();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.PCDD,
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
                return base.LotteryList.Contains(LotteryEnum.PCDD) && base.DataSourceList.Contains(DataSourceEnum.Robo);
            }
        }
    }
}
