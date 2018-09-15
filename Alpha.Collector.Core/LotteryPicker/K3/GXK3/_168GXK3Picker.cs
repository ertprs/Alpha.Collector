using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 168开奖网抓取广西快3
    /// </summary>
    internal class _168GXK3Picker : BasePicker, IPicker, IGXK3Picker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "https://api.api68.com/lotteryJSFastThree/getJSFastThreeList.do?date=&lotCode=10026";

        /// <summary>
        /// 执行抓取
        /// </summary>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                _168Picker picker = new _168Picker(URL, LotteryEnum.GXK3);
                List<OpenResult> resultList = picker.Pick();
                return (from o in resultList
                        select new OpenResult
                        {
                            create_time = DateTime.Now,
                            open_time = o.open_time,
                            lottery_code = o.lottery_code,
                            issue_number = Convert.ToInt64("20" + o.issue_number),
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
                    lottery_code = LotteryEnum.GXK3,
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
                return base.LotteryList.Contains(LotteryEnum.GXK3) && base.DataSourceList.Contains(DataSourceEnum._168);
            }
        }
    }
}
