using System;
using System.Collections.Generic;
using Alpha.Collector.Model;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 彩世界北京PK10抓取工具
    /// </summary>
    internal class CSJBJKCPicker : BasePicker, IPicker, IBJKCPicker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "https://www.1396j.com/pk10/kaijiang";

        private CSJPicker _picker;

        public CSJBJKCPicker()
        {
            this._picker = new CSJPicker(URL, LotteryEnum.BJKC);
        }

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                return this._picker.Pick();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.BJKC,
                    data_source = DataSourceEnum.CSJ,
                    log_message = ex.ToString()
                };
                AlphaLogManager.Error(appLog);

                return new List<OpenResult>();
            }
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get
            {
                return base.LotteryList.Contains(LotteryEnum.BJKC) && base.DataSourceList.Contains(DataSourceEnum.CSJ);
            }
        }
    }
}
