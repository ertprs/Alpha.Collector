using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 湖南快乐十分采集器管理器
    /// </summary>
    public class HNKLSFPickerManager : PickerManager
    {
        /// <summary>
        /// 获取采集器
        /// </summary>
        /// <param name="dataSource"></param>
        public override IPicker GetPicker(string dataSource)
        {
            switch (dataSource)
            {
                case DataSource.ROBO:
                    return (IPicker)Activator.CreateInstance(typeof(RoboHNKLSFPicker));
                default:
                    return null;
            }
        }
    }
}
