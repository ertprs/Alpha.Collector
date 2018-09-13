using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 重庆幸运农场采集器管理器
    /// </summary>
    public class CQXYNCPickerManager : PickerManager
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
                    return (IPicker)Activator.CreateInstance(typeof(_168CQXYNCPicker));
                case DataSource.ROBO:
                    return (IPicker)Activator.CreateInstance(typeof(RoboCQXYNCPicker));
                case DataSource.BiFa:
                    return (IPicker)Activator.CreateInstance(typeof(BiFaCQXYNCPicker));
                default:
                    return null;
            }
        }
    }
}
