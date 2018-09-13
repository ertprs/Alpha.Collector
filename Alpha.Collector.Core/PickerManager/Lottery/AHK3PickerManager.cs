using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 安徽快3采集器管理器
    /// </summary>
    public class AHK3PickerManager : PickerManager
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
                    return (IPicker)Activator.CreateInstance(typeof(_168AHK3Picker));
                case DataSource.KCZX:
                    return (IPicker)Activator.CreateInstance(typeof(KCAHK3Picker));
                case DataSource.ROBO:
                    return (IPicker)Activator.CreateInstance(typeof(RoboAHK3Picker));
                default:
                    return null;
            }
        }
    }
}
