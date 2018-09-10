using Alpha.Collector.Core;
using Alpha.Collector.Dao;
using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Collector.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<OpenResult> resultList = CQSSCCollectorManager.GetInstance(DataSourceEnum._168).Run();
            List<OpenResult> resultList = CQSSCPickerManager.Run(4);
            int result = OpenResultDAO.Insert(resultList);

            AppLog appLog = new AppLog
            {
                create_time = DateTime.Now,
                log_type = "INFO",
                lottery_code = LotteryType.CQSSC,
                log_message = "抓取到重庆时时彩开奖结果：",
                data_source = DataSource._168
            };

            AlphaLogManager.Info(appLog);

        }
    }
}
