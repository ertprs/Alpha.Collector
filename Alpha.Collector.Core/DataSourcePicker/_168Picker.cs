using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 168开奖网采集器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class _168Picker
    {
        private string _url;
        private string _lotteryCode;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="lotteryCode"></param>
        public _168Picker(string url, string lotteryCode)
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
            _168Response response = json.ToEntity<_168Response>();
            if (response.errorCode != 0)
            {
                throw new Exception($"从168开奖网采集{this._lotteryCode}出错。错误代码：{response.errorCode}。抓取地址：{this._url}");
            }

            if (response.result.data == null || response.result.data.Count == 0)
            {
                throw new Exception($"从168开奖网采集{this._lotteryCode}出错。抓取地址：{this._url}");
            }

            return (from o in response.result.data
                    select new OpenResult
                    {
                        create_time = DateTime.Now,
                        open_time = DateTime.Parse(o.preDrawTime),
                        lottery_code = this._lotteryCode,
                        issue_number = Convert.ToInt64(o.preDrawIssue),
                        open_data = o.preDrawCode,
                        data_source = DataSourceEnum._168
                    }).OrderBy(o => o.issue_number).ToList();
        }
    }
}
