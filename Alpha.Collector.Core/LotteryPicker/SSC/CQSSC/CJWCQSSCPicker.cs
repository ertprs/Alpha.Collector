using System.Collections.Generic;
using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 彩经网采集重庆时时彩
    /// </summary>
    internal class CJWCQSSCPicker : BasePicker, IPicker, ICQSSCPicker
    {
        /// <summary>
        /// 抓取地址
        /// </summary>
        private const string URL = "https://shishicai.cjcp.com.cn/chongqing/kaijiang/";

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                CJWSSCPicker picker = new CJWSSCPicker(LotteryEnum.CQSSC, URL);
                return picker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.CQSSC,
                    data_source = DataSourceEnum.CJW,
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
                return base.LotteryList.Contains(LotteryEnum.CQSSC) && base.DataSourceList.Contains(DataSourceEnum.CJW);
            }
        }
    }
}
