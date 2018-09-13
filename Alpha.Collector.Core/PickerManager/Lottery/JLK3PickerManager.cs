using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 吉林快3采集器管理器
    /// </summary>
    public class JLK3PickerManager : PickerManager
    {
        /// <summary>
        /// 获取采集器
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public override IPicker GetPicker(string dataSource)
        {
            switch (dataSource)
            {
                case DataSource._168:
                    return (IPicker)Activator.CreateInstance(typeof(_168JLK3Picker));
                case DataSource.KCZX:
                    return (IPicker)Activator.CreateInstance(typeof(KCJLK3Picker));
                case DataSource.ROBO:
                    return (IPicker)Activator.CreateInstance(typeof(RoboJLK3Picker));
                default:
                    return null;
            }
        }
    }
}
