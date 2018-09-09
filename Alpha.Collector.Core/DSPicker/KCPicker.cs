using Alpha.Collector.Utils;
using Alpha.Collector.Model;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 快彩在线采集器
    /// </summary>
    public class KCPicker
    {
        private int _lotteryCode;

        /// <summary>
        /// 大发彩票开奖结果采集
        /// </summary>
        /// <param name="lotteryCode"></param>
        public KCPicker(int lotteryCode)
        {
            this._lotteryCode = lotteryCode;
        }

        /// <summary>
        /// 采集开奖结果
        /// </summary>
        /// <returns></returns>
        public List<OpenResult> Pick()
        {
            HttpRequestParam param = this.GetParam(this._lotteryCode);

            string errorInfo = string.Empty;
            string html = HttpHelper.GetHtml(param, ref errorInfo);
            if (!string.IsNullOrEmpty(errorInfo))
            {
                throw new Exception($"从快彩在线采集{this._lotteryCode}出错。错误信息：{errorInfo}，抓取地址：{param.Url}");
            }

            if (string.IsNullOrEmpty(html))
            {
                throw new Exception($"从快彩在线采集{this._lotteryCode}出错。抓取地址：{param.Url}");
            }

            if (html.ToLower().Contains("robots"))
            {
                throw new Exception($"从快彩在线采集{this._lotteryCode}出错。返回的html错误（被反爬虫截断请求）。HTML：{html}");
            }

            KCResponse response = JsonHelper.JsonToEntity<KCResponse>(html);
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

        /// <summary>
        /// 根据彩票类型获取参数
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <returns></returns>
        private HttpRequestParam GetParam(int lotteryCode)
        {
            HttpRequestParam param = new HttpRequestParam
            {
                Origin = "http://www.cpkk7.com",
                Methond = "POST",
                Url = "http://www.cpkk7.com/tools/ssc_ajax.ashx?A=GetLotteryOpen&S=kczx&U=dw9527"
            };

            if (lotteryCode <= 0)
            {
                return param;
            }

            param.PostData = "Action=GetLotteryOpen&LotteryCode=" + lotteryCode + "&IssueNo=0&DataNum=5&SourceName=PC";

            return param;
        }
    }
}

