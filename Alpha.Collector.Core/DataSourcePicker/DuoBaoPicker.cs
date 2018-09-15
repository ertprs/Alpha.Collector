using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 多宝采集器
    /// </summary>
    public class DuoBaoPicker
    {
        private string _url;
        private string _lotteryCode;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="lotteryCode"></param>
        public DuoBaoPicker(string url, string lotteryCode)
        {
            this._url = url;
            this._lotteryCode = lotteryCode;
        }

        /// <summary>
        /// 抓取
        /// </summary>
        /// <returns></returns>
        public List<OpenResult> Pick()
        {
            string json = HttpHelper.HttpGet(this._url);
            DuoBaoReponse response = json.ToEntity<DuoBaoReponse>();
            if (response == null || response.list == null)
            {
                throw new Exception($"从多宝采集{this._lotteryCode}没有抓取异常。抓取地址：{this._url}");
            }

            if (response.list.Count == 0)
            {
                throw new Exception($"从多宝采集{this._lotteryCode}没有抓取到结果。抓取地址：{this._url}");
            }

            return (from r in response.list
                    select new OpenResult
                    {
                        create_time = DateTime.Now,
                        lottery_code = this._lotteryCode,
                        issue_number = r.c_t,
                        open_time = r.c_d,
                        open_data = r.c_r,
                        data_source = DataSourceEnum.DuoBao
                    }).OrderBy(r => r.issue_number).ToList();
        }
    }
}
