using Alpha.Collector.Core;
using Alpha.Collector.Dao;
using Alpha.Collector.Model;
using Alpha.Collector.Service;
using Alpha.Collector.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Collector.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IPicker> list = LotteryHelper.GetPickerList(LotteryEnum.JSK3);

            MainWork work = new MainWork();
            work.Run();

            //List<OpenResult> resultList;
            //PickerManager manager = new JSK3PickerManager();
            //List<IPicker> pickerList = manager.GetPickerList();
            //IPicker picker = manager.GetPicker(DataSourceEnum.DuoBao);
            //resultList = picker.Run();

            //int result = OpenResultApp.Insert(resultList);

            Console.Read();
        }
    }
}
