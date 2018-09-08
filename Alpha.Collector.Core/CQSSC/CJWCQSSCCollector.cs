using System.Collections.Generic;
using Alpha.Collector.Model;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 财经网采集重庆时时彩
    /// </summary>
    internal class CJWCQSSCCollector : ICQSSCCollector
    {
        /// <summary>
        /// 抓取地址
        /// </summary>
        private const string URL = "https://shishicai.cjcp.com.cn/chongqing/kaijiang/";

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> ICQSSCCollector.Run()
        {
            CJWPicker picker = new CJWPicker(LotteryCodeEnum.CQSSC, URL);
            return picker.Pick();
        }
    }
}
