using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 168开奖网采集器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class _168Picker<T>
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
        public List<T> Pick()
        {
            string json = HttpHelper.HttpGet(this._url);
            _168Response<T> response = JsonHelper.JsonToEntity<_168Response<T>>(json, null);
            if (response.errorCode != 0)
            {
                throw new Exception($"从168开奖网采集{this._lotteryCode}出错。错误代码：{response.errorCode}。抓取地址：{this._url}");
            }

            if (response.result.data == null || response.result.data.Count == 0)
            {
                throw new Exception($"从168开奖网采集{this._lotteryCode}出错。抓取地址：{this._url}");
            }

            return response.result.data;
        }
    }
}
