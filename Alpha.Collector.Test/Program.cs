using Alpha.Collector.Core;
using Alpha.Collector.Dao;
using Alpha.Collector.Model;
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
        private static ILog log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            List<OpenResult> resultList = CQSSCCollectorManager.GetInstance(DataSourceEnum.KCZX).Run();
            //List<OpenResult> resultList = CQSSCCollectorManager.Run(4);
            int result = OpenResultDAO.Insert(resultList);
            log.Info("抓取到重庆时时彩开奖结果：" + result);

            //var o = MySqlHelper.GetList<OpenResult>("select * from open_result");
        }
    }
}
