using System.Collections.Generic;
using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 多宝北京赛车采集器
    /// </summary>
    internal class DuoBaoBJKCPicker : BasePicker, IPicker, IBJKCPicker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "http://www.duobaopk.com/index.php?c=api&a=updateinfo&cp=bjpk10&uptime=1536900456&chtime=305&catid=2&modelid=9";

        /// <summary>
        /// 抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                DuoBaoPicker picker = new DuoBaoPicker(URL, LotteryEnum.BJKC);
                return picker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.BJKC,
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
                return base.LotteryList.Contains(LotteryEnum.BJKC) && base.DataSourceList.Contains(DataSourceEnum.DuoBao);
            }
        }
    }
}
