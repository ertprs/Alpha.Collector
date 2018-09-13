using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 广西快乐十分采集器管理器
    /// </summary>
    public class GXKLSFPickerManager : PickerManager
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
                    return (IPicker)Activator.CreateInstance(typeof(_168GXKLSFPicker));
                case DataSource.ROBO:
                    return (IPicker)Activator.CreateInstance(typeof(RoboGXKLSFPicker));
                default:
                    return null;
            }
        }
    }
}
