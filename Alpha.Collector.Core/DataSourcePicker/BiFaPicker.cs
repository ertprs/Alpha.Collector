using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 必发采集器
    /// </summary>
    public class BiFaPicker
    {
        private string _url;
        private string _lotteryType;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="lotteryType"></param>
        public BiFaPicker(string url, string lotteryType)
        {
            this._url = url;
            this._lotteryType = lotteryType;
        }

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        public List<OpenResult> Pick()
        {
            string json = HttpHelper.HttpGet(this._url);
            List<BiFaResponse> responseList = JsonHelper.ToEntity<List<BiFaResponse>>(json);
            return (from r in responseList
                    select new OpenResult
                    {
                        create_time = DateTime.Now,
                        open_time = r.openTime,
                        lottery_code = this._lotteryType,
                        issue_number = Convert.ToInt64(r.turnNum),
                        open_data = r.openNum,
                        data_source = DataSourceEnum.BiFa
                    }).OrderBy(o => o.issue_number).ToList();
        }
    }
}
