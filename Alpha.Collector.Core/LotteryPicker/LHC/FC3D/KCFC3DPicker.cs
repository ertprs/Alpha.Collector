using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 快彩在线采集福彩3D
    /// </summary>
    public class KCFC3DPicker : IPicker
    {
        private KCPicker _kcPicker;

        public KCFC3DPicker()
        {
            this._kcPicker = new KCPicker(LotteryType.FC3D);
        }

        /// <summary>
        /// 执行抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                List<OpenResult> dataList = this._kcPicker.Pick();
                return (from o in dataList
                        select new OpenResult
                        {
                            create_time = o.create_time,
                            open_time = o.open_time,
                            lottery_code = LotteryType.FC3D,
                            issue_number = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd") + o.issue_number.ToString().Replace(DateTime.Now.ToString("yyyy"), "")),
                            open_data = o.open_data,
                            data_source = o.data_source
                        }).OrderBy(o => o.issue_number).ToList();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogType.ERROR,
                    lottery_code = LotteryType.FC3D,
                    data_source = DataSource.KCZX,
                    log_message = ex.ToString()
                };
                AlphaLogManager.Error(appLog);

                return new List<OpenResult>();
            }
        }
    }
}
