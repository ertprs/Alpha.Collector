using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 广东快乐十分采集器管理器
    /// </summary>
    public class GDKLSFPickerManager : PickerManager
    {
        /// <summary>
        /// 获取采集器
        /// </summary>
        /// <param name="collector"></param>
        public override IPicker GetPicker(string dataSource)
        {
            switch (dataSource)
            {
                case DataSource._168:
                    return (IPicker)Activator.CreateInstance(typeof(_168GDKLSFPicker));
                case DataSource.ROBO:
                    return (IPicker)Activator.CreateInstance(typeof(RoboGDKLSFPicker));
                case DataSource.BiFa:
                    return (IPicker)Activator.CreateInstance(typeof(BiFaGDKLSFPicker));
                default:
                    return null;
            }
        }
    }
}
