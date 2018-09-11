using Alpha.Collector.Model;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 彩经网抓取天津时时彩
    /// </summary>
    internal class CJWTJSSCPicker : IPicker
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
                CJWSSCPicker picker = new CJWSSCPicker(LotteryType.TJSSC, URL);
                return picker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogType.ERROR,
                    lottery_code = LotteryType.TJSSC,
                    data_source = DataSource.CJW,
                    log_message = ex.ToString()
                };
                AlphaLogManager.Error(appLog);

                return new List<OpenResult>();
            }
        }
    }
}
