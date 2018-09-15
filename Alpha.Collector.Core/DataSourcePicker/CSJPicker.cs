using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 彩世界采集器
    /// </summary>
    public class CSJPicker
    {
        private string _url;
        private string _lotteryType;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="lotteryType"></param>
        public CSJPicker(string url, string lotteryType)
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
            HttpRequestParam param = new HttpRequestParam { Url = this._url };
            string errorInfo = string.Empty;
            string html = HttpHelper.GetHtml(param, ref errorInfo);
            if (!string.IsNullOrEmpty(errorInfo))
            {
                throw new Exception($"从彩世界采集{this._lotteryType}出错。错误信息：{errorInfo}，抓取地址：{this._url}");
            }

            Regex regex = new Regex(@"<table[^><]*class=""history|tbHistory|dataContainer""[^><]*>(?<html>[\S\s]*?</table>", RegexOptions.IgnoreCase);
            Match match = regex.Match(html);
            if (!match.Success)
            {
                throw new Exception($"从彩世界采集{this._lotteryType}出错。抓取地址：{this._url}，源代码：{html}");
            }

            html = match.Groups["html"].Value;

            regex = new Regex(@"<tr>\s*<td>\s*<i[^><]*class=""font_gray666"">(?<issueNo>[^><]+)</i>\s*<i[^><]*class=""font_gray999"">(?<time>[^><]+)</i>\s*</td>\s*<td>\s*<div[^><]*>(?<num>[\s\S]*?)</div>", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(html);
            if (matches.Count == 0)
            {
                throw new Exception($"从彩世界采集{this._lotteryType}出错。抓取地址：{this._url}，源代码：{html}");
            }

            List<OpenResult> resultList = new List<OpenResult>();
            foreach (Match m in matches)
            {
                OpenResult result = new OpenResult
                {
                    create_time = DateTime.Now,
                    lottery_code = this._lotteryType,
                    data_source = DataSourceEnum.CSJ
                };

                result.issue_number = Convert.ToInt64(m.Groups["issueNo"].Value.Replace("-", ""));
                result.open_time = Convert.ToDateTime(m.Groups["time"].Value);
                result.open_data = this.GetOpenData(m.Groups["num"].Value);

                if (string.IsNullOrEmpty(result.open_data))
                {
                    continue;
                }

                resultList.Add(result);
            }

            return resultList;
        }

        /// <summary>
        /// 解析开奖号码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetOpenData(string value)
        {
            Regex regex = new Regex(@"<span[^><]*>(?<num>[0-9]+)</span>", RegexOptions.IgnoreCase);
            MatchCollection macthes = regex.Matches(value);
            if (macthes.Count == 0)
            {
                return "";
            }

            string result = string.Empty;
            foreach (Match match in macthes)
            {
                result += match.Groups["num"].Value.Trim() + ",";
            }

            return result.TrimEnd(',');
        }
    }
}
