using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 快彩在线抓取重庆时时彩
    /// </summary>
    internal class KCCQSSCCollector : ICQSSCCollector
    {
        private KCPicker _kcPicker;

        public KCCQSSCCollector()
        {
            this._kcPicker = new KCPicker(1000);
        }

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> ICQSSCCollector.Run()
        {
            try
            {
                KCResponse response = this._kcPicker.Pick();
                if (response.Code < 1)
                {
                    return new List<OpenResult>();
                }

                if (response.BackData == null || response.BackData.Count == 0)
                {
                    return new List<OpenResult>();
                }

                return (from o in response.BackData
                        select new OpenResult
                        {
                            create_time = DateTime.Now,
                            open_time = DateTime.Parse(o.OpenTime),
                            lottery_code = LotteryCodeEnum.CQSSC,
                            issue_number = Convert.ToInt64(o.IssueNo),
                            open_data = o.LotteryOpen,
                            data_source = DataSourceEnum.KCZX
                        }).OrderBy(o => o.issue_number).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
