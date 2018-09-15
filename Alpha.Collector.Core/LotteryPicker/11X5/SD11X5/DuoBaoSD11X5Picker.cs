using System.Collections.Generic;
using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 多宝山东11选5采集器
    /// </summary>
    internal class DuoBaoSD11X5Picker : BasePicker, IPicker, ISD11X5Picker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "http://www.duobaopk.com/index.php?c=api&a=updateinfo&cp=sd11x5&uptime=1536903308&chtime=605&catid=188&modelid=25";

        /// <summary>
        /// 抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                DuoBaoPicker picker = new DuoBaoPicker(URL, LotteryEnum.SD11X5);
                return picker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.SD11X5,
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
                return base.LotteryList.Contains(LotteryEnum.SD11X5) && base.DataSourceList.Contains(DataSourceEnum.DuoBao);
            }
        }
    }
}
