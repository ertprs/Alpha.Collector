using Alpha.Collector.Model;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 彩经网抓取天津时时彩
    /// </summary>
    internal class CJWTJSSCPicker : BasePicker, IPicker, ITJSSCPicker
    {
        private const string URL = "https://shishicai.cjcp.com.cn/tianjin/kaijiang/?t={$random}";

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                CJWSSCPicker picker = new CJWSSCPicker(LotteryEnum.TJSSC, URL);
                return picker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.TJSSC,
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
                return base.LotteryList.Contains(LotteryEnum.TJSSC) && base.DataSourceList.Contains(DataSourceEnum.CJW);
            }
        }
    }
}
