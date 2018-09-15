using System.Collections.Generic;
using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 多宝重庆时时彩采集器
    /// </summary>
    internal class DuoBaoCQSSCPicker : BasePicker, IPicker, ICQSSCPicker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "http://www.duobaopk.com/index.php?c=api&a=updateinfo&cp=cqssc&uptime=1536900533&chtime=605&catid=3&modelid=10";

        /// <summary>
        /// 抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                DuoBaoPicker picker = new DuoBaoPicker(URL, LotteryEnum.CQSSC);
                return picker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.CQSSC,
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
                return base.LotteryList.Contains(LotteryEnum.CQSSC) && base.DataSourceList.Contains(DataSourceEnum.DuoBao);
            }
        }
    }
}
