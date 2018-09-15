using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 乐博采集器
    /// </summary>
    public class RoboPicker
    {
        private const string URL = "http://robo33.com/NewLottery/GetLotteryResult?type={0}";
        private string _url;
        private string _lotteryType;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lotteryCode"></param>
        public RoboPicker(string lotteryCode)
        {
            this._url = string.Format(URL, lotteryCode);
            this._lotteryType = lotteryCode;

            if (lotteryCode == LotteryEnum.HNKLSF)
            {
                this._url = string.Format(URL, "hunanklsf");
            }

            if (lotteryCode == LotteryEnum.JSK3)
            {
                this._url = string.Format(URL, "nmk3");
            }

            if (lotteryCode == LotteryEnum.HuBK3)
            {
                this._url = string.Format(URL, "hbk3");
            }

            if (lotteryCode == LotteryEnum.XGLHC)
            {
                this._url = string.Format(URL, "lhc");
            }
        }

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        public List<OpenResult> Pick()
        {
            //TODO:乐博有问题暂时
            return new List<OpenResult>();

            string json = HttpHelper.HttpGet(this._url);
            RoboResponse response = JsonHelper.ToEntity<RoboResponse>(json);
            if (!response.Success)
            {
                throw new Exception($"从乐博采集{this._lotteryType}出错。抓取地址：{this._url}");
            }

            return (from r in response.Data
                    select new OpenResult
                    {
                        create_time = DateTime.Now,
                        open_time = DateTime.Parse(r.openDate),
                        lottery_code = this._lotteryType,
                        issue_number = Convert.ToInt64(r.formatStage),
                        open_data = r.result,
                        data_source = DataSourceEnum.Robo
                    }).OrderBy(o => o.issue_number).ToList();
        }
    }
}
