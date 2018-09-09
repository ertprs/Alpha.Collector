using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 财经网采集器
    /// </summary>
    public class CJWPicker
    {
        private string _lotteryType;
        private string _url;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lotteryType"></param>
        /// <param name="url"></param>
        public CJWPicker(string lotteryType, string url)
        {
            this._lotteryType = lotteryType;
            this._url = url;
        }

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <param name="lotteryType"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public List<OpenResult> Pick()
        {
            HttpRequestParam param = new HttpRequestParam { Url = this._url };
            string errorInfo = string.Empty;
            string html = HttpHelper.GetHtml(param, ref errorInfo);
            if (!string.IsNullOrEmpty(errorInfo))
            {
                throw new Exception($"从财经网采集{this._lotteryType}出错。错误信息：{errorInfo}，抓取地址：{this._url}");
            }

            Regex regex = new Regex(@"<table[^><]*class=""kjjg_table"">(?<html>[\S\s]*?)</table>", RegexOptions.IgnoreCase);
            Match match = regex.Match(html);
            if (!match.Success)
            {
                throw new Exception($"从财经网采集{this._lotteryType}出错。抓取地址：{this._url}，源代码：{html}");
            }

            html = match.Groups["html"].Value;

            regex = new Regex(@"<tr>\s*<td>(?<issueNo>[0-9]+)[^><]*</td>\s*<td>(?<time>[0-9-\s:]+)</td>(?<num>\s*<td>[\s\S]*?</td>\s*)</tr>", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(html);
            if (matches.Count == 0)
            {
                throw new Exception($"从财经网采集{this._lotteryType}出错。抓取地址：{this._url}，源代码：{html}");
            }

            List<OpenResult> resultList = new List<OpenResult>();
            foreach (Match m in matches)
            {
                OpenResult result = new OpenResult
                {
                    create_time = DateTime.Now,
                    lottery_code = this._lotteryType,
                    data_source = DataSourceEnum.CJW
                };

                result.issue_number = Convert.ToInt64(m.Groups["issueNo"].Value);
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
        /// 获取开奖号码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetOpenData(string value)
        {
            Regex regex = new Regex(@"<div\s*class=""hm_bg"">(?<num>[0-9]+)</div>", RegexOptions.IgnoreCase);
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
