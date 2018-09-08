using Alpha.Collector.Utils;
using Alpha.Collector.Model;

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
        public KCResponse Pick()
        {
            HttpRequestParam param = this.GetParam(this._lotteryCode);

            if (string.IsNullOrEmpty(param.Url))
            {
                return new KCResponse
                {
                    Code = -1,
                    StrCode = "请求地址不能为空"
                };
            }

            if (string.IsNullOrEmpty(param.PostData))
            {
                return new KCResponse
                {
                    Code = -1,
                    StrCode = "请求参数不能为空"
                };
            }

            string errorInfo = string.Empty;
            string html = HttpHelper.GetHtml(param, ref errorInfo);
            if (!string.IsNullOrEmpty(errorInfo))
            {
                return new KCResponse
                {
                    Code = -1,
                    StrCode = errorInfo
                };
            }

            if (string.IsNullOrEmpty(html))
            {
                return new KCResponse
                {
                    Code = -1,
                    StrCode = "返回的html为空。Url：" + param.Url
                };
            }

            if (html.ToLower().Contains("robots"))
            {
                return new KCResponse
                {
                    Code = -1,
                    StrCode = "返回的html错误（被反爬虫截断请求）。HTML：" + html
                };
            }

            KCResponse KCResponse = JsonHelper.JsonToEntity<KCResponse>(html);

            return KCResponse;
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

