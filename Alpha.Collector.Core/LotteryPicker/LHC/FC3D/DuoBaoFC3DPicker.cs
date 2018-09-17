using System.Collections.Generic;
using Alpha.Collector.Model;
using System;
using System.Linq;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 多宝福彩3D采集器
    /// </summary>
    internal class DuoBaoFC3DPicker : BasePicker, IPicker, IFC3DPicker
    {
        /// <summary>
        /// 采集地址
        /// </summary>
        private const string URL = "http://www.duobaopk.com/index.php?c=api&a=updateinfo&cp=fc3d&uptime=1536903551&chtime=86405&catid=170&modelid=23";

        /// <summary>
        /// 抓取
        /// </summary>
        /// <returns></returns>
        List<OpenResult> IPicker.Run()
        {
            try
            {
                DuoBaoPicker picker = new DuoBaoPicker(URL, LotteryEnum.FC3D);
                List<OpenResult> dataList = picker.Pick();
                return (from o in dataList
                        select new OpenResult
                        {
                            create_time = DateTime.Now,
                            open_time = o.open_time,
                            lottery_code = o.lottery_code,
                            issue_number = Convert.ToInt64(o.open_time.ToString("yyyyMMdd") + o.issue_number.ToString().Replace(o.open_time.ToString("yyyy"), "")),
                            open_data = o.open_data,
                            data_source = DataSourceEnum._168
                        }).OrderBy(o => o.issue_number).ToList();
            }
            catch (Exception ex)
            {
                AppLog appLog = new AppLog
                {
                    create_time = DateTime.Now,
                    log_type = LogTypeEnum.ERROR,
                    lottery_code = LotteryEnum.FC3D,
                    data_source = DataSourceEnum.DuoBao,
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
                return base.LotteryList.Contains(LotteryEnum.FC3D) && base.DataSourceList.Contains(DataSourceEnum.DuoBao);
            }
        }
    }
}
