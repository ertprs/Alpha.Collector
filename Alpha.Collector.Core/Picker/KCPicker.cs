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
        private string _lotteryType;

        /// <summary>
        /// 大发彩票开奖结果采集
        /// </summary>
        /// <param name="lotteryType"></param>
        public KCPicker(string lotteryType)
        {
            this._lotteryType = lotteryType;
            this._lotteryCode = this.GetLotteryCode(lotteryType);
        }

        /// <summary>
        /// 根据彩种名字，获取彩种编号
        /// </summary>
        /// <param name="lotteryType"></param>
        /// <returns></returns>
        private int GetLotteryCode(string lotteryType)
        {
            switch (lotteryType)
            {
                case LotteryType.CQSSC:
                    return 1000;
                case LotteryType.TJSSC:
                    return 1003;
                case LotteryType.XJSSC:
                    return 1001;

                case LotteryType.JSK3:
                    return 1401;
                case LotteryType.ANK3:
                    return 1402;
                case LotteryType.GXK3:
                    return 1404;
                case LotteryType.HuBK3:
                    return 1405;
                case LotteryType.BJK3:
                    return 1406;
                case LotteryType.HeBK3:
                    return 1408;
                case LotteryType.GSK3:
                    return 1411;
                case LotteryType.SHK3:
                    return 1410;
                case LotteryType.GZK3:
                    return 1409;
                case LotteryType.JLK3:
                    return 1404;

                case LotteryType.BJKL8:
                    return 1302;
                case LotteryType.BJKC:
                    return 1303;
                case LotteryType.XGLHC:
                    return 1301;

                case LotteryType.GD11X5:
                    return 1100;
                case LotteryType.SH11X5:
                    return 1101;
                case LotteryType.SD11X5:
                    return 1102;
                case LotteryType.JX11X5:
                    return 1103;

                case LotteryType.FC3D:
                    return 1201;
                case LotteryType.PL3:
                    return 1202;

                default:
                    return -1;
            }
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
                throw new Exception($"从快彩在线采集{this._lotteryType}出错。错误信息：{errorInfo}，抓取地址：{param.Url}");
            }

            if (string.IsNullOrEmpty(html))
            {
                throw new Exception($"从快彩在线采集{this._lotteryType}出错。抓取地址：{param.Url}");
            }

            if (html.ToLower().Contains("robots"))
            {
                throw new Exception($"从快彩在线采集{this._lotteryType}出错。返回的html错误（被反爬虫截断请求）。HTML：{html}");
            }

            KCResponse response = html.ToEntity<KCResponse>();
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
                        lottery_code = _lotteryType,
                        issue_number = Convert.ToInt64(o.IssueNo),
                        open_data = o.LotteryOpen,
                        data_source = DataSource.KCZX
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

