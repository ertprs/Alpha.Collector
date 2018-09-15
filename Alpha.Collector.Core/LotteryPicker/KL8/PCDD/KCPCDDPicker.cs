using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 快彩在线采集PC蛋蛋
    /// </summary>
    public class KCPCDDPicker : BasePicker, IPicker, IPCDDPicker
    {
        private KCPicker _kcPicker;

        public KCPCDDPicker()
        {
            this._kcPicker = new KCPicker(LotteryEnum.BJKL8);
        }

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                List<OpenResult> list = this._kcPicker.Pick();
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
                return base.LotteryList.Contains(LotteryEnum.PCDD) && base.DataSourceList.Contains(DataSourceEnum.KC);
            }
        }
    }
}
