using Alpha.Collector.Model;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 彩经网抓取新疆时时彩
    /// </summary>
    internal class CJWXJSSCPicker : BasePicker, IPicker, IXJSSCPicker
    {
        private const string URL = "https://shishicai.cjcp.com.cn/xinjiang/kaijiang/?t={$random}";

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                CJWSSCPicker picker = new CJWSSCPicker(LotteryEnum.XJSSC, URL);
                return picker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.XJSSC,
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
                return base.LotteryList.Contains(LotteryEnum.XJSSC) && base.DataSourceList.Contains(DataSourceEnum.CJW);
            }
        }
    }
}
