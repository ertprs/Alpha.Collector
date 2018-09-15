using System.Collections.Generic;
using Alpha.Collector.Model;
using System;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 多宝体彩排列3采集器
    /// </summary>
    internal class DuoBaoTCPL3Picker : BasePicker, IPicker, ITCPL3Picker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "http://www.duobaopk.com/index.php?c=api&a=updateinfo&cp=pl3&uptime=1536903660&chtime=86405&catid=178&modelid=24";

        /// <summary>
        /// 抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                DuoBaoPicker picker = new DuoBaoPicker(URL, LotteryEnum.TCPL3);
                List<OpenResult> dataList = picker.Pick();
                return (from o in dataList
                        select new OpenResult
                        {
                            create_time = DateTime.Now,
                            open_time = o.open_time,
                            lottery_code = o.lottery_code,
                            issue_number = Convert.ToInt64(o.issue_number.ToString().Replace(o.open_time.ToString("yyyy"), o.open_time.ToString("yyyyMMdd"))),
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
                    data_source = DataSourceEnum.DuoBao,
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
                return base.LotteryList.Contains(LotteryEnum.TCPL3) && base.DataSourceList.Contains(DataSourceEnum.DuoBao);
            }
        }
    }
}
