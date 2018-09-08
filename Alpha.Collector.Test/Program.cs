using Alpha.Collector.CQSSC;
using Alpha.Collector.Dao;
using Alpha.Collector.Model.DataBase;
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
            ICQSSCCollector collector = new _168CQSSHCollector();
            List<OpenResult> resultList = collector.Run();
            int result = OpenResultDAO.Insert(resultList);
            log.Info("抓取到重庆时时彩开奖结果：" + result);
            //var o = MySqlHelper.GetList<OpenResult>("select * from open_result");
        }
    }
}
