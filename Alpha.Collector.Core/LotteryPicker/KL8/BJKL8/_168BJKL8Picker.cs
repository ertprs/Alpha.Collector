using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 168开奖网抓取北京快乐8
    /// </summary>
    internal class _168BJKL8Picker : BasePicker, IPicker, IBJKL8Picker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "https://api.api68.com/LuckTwenty/getBaseLuckTwentyList.do?date=&lotCode=10014";

        /// <summary>
        /// 执行抓取
        /// </summary>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                _168Picker picker = new _168Picker(URL, LotteryEnum.BJKL8);
                List<OpenResult> dataList = picker.Pick();

                return (from o in dataList
                        select new OpenResult
                        {
                            create_time = DateTime.Now,
                            open_time = o.open_time,
                            lottery_code = LotteryEnum.BJKL8,
                            issue_number = o.issue_number,
                            open_data = o.open_data.Substring(0, o.open_data.Length - 2),
                            data_source = DataSourceEnum._168
                        }).OrderBy(o => o.issue_number).ToList();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.BJKL8,
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
                return base.LotteryList.Contains(LotteryEnum.BJKL8) && base.DataSourceList.Contains(DataSourceEnum._168);
            }
        }
    }
}
