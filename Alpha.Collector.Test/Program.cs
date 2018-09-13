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
            List<OpenResult> resultList;
            IPickerManager manager = new BJKCPickerManager();
            resultList = manager.GetPicker(DataSource._168).Run();

            int result = OpenResultApp.Insert(resultList);
        }
    }
}
