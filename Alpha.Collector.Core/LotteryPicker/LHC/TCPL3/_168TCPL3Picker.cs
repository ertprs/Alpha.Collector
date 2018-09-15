using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 168开奖网抓取体彩排列3
    /// </summary>
    internal class _168TCPL3Picker : BasePicker, IPicker, ITCPL3Picker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "https://api.api68.com/QuanGuoCai/getLotteryInfoList.do?lotCode=10043";

        /// <summary>
        /// 执行抓取
        /// </summary>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                _168Picker picker = new _168Picker(URL, LotteryEnum.TCPL3);
                List<OpenResult> dataList = picker.Pick();

                return (from o in dataList
                        select new OpenResult
                        {
                            create_time = DateTime.Now,
                            open_time = o.open_time,
                            lottery_code = o.lottery_code,
                            issue_number = Convert.ToInt64(("20" + o.issue_number).Replace(o.open_time.ToString("yyyy"), o.open_time.ToString("yyyyMMdd"))),
                            open_data = o.open_data,
                            data_source = DataSourceEnum._168
                        }).OrderBy(o => o.issue_number).ToList();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.TCPL3,
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
                return base.LotteryList.Contains(LotteryEnum.TCPL3) && base.DataSourceList.Contains(DataSourceEnum._168);
            }
        }
    }
}
