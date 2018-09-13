using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 福彩3D采集器管理器
    /// </summary>
    public class FC3DPickerManager : PickerManager
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
                    return (IPicker)Activator.CreateInstance(typeof(_168FC3DPicker));
                case DataSource.KCZX:
                    return (IPicker)Activator.CreateInstance(typeof(KCFC3DPicker));
                case DataSource.ROBO:
                    return (IPicker)Activator.CreateInstance(typeof(RoboFC3DPicker));
                case DataSource.BiFa:
                    return (IPicker)Activator.CreateInstance(typeof(BiFaFC3DPicker));
                default:
                    return null;
            }
        }
    }
}
