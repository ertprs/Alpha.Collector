using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 必发抓取重庆幸运农场
    /// </summary>
    internal class BiFaCQXYNCPicker : BasePicker, IPicker, ICQXYNCPicker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "https://www.bf668.com/static//data/2018091261HistoryLottery.json?_={0}";

        /// <summary>
        /// 执行抓取
        /// </summary>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                long timeStamp = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                BiFaPicker picker = new BiFaPicker(string.Format(URL, timeStamp), LotteryEnum.CQXYNC);
                List<OpenResult> dataList = picker.Pick();

                return (from o in dataList
                        select new OpenResult
                        {
                            create_time = o.create_time,
                            open_time = o.open_time,
                            lottery_code = LotteryEnum.CQXYNC,
                            issue_number = Convert.ToInt64("20" + o.issue_number.ToString()),
                            open_data = o.open_data,
                            data_source = o.data_source
                        }).OrderBy(o => o.issue_number).ToList();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.CQXYNC,
                    data_source = DataSourceEnum.BiFa,
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
                return base.LotteryList.Contains(LotteryEnum.CQXYNC) && base.DataSourceList.Contains(DataSourceEnum.BiFa);
            }
        }
    }
}
