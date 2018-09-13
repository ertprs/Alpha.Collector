using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 香港六合彩采集器管理器
    /// </summary>
    public class XGLHCPickerManager : PickerManager
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
                //case DataSource.BiFa:
                //    return (IPicker)Activator.CreateInstance(typeof(BiFaXGLHCPicker));
                case DataSource.KCZX:
                    return (IPicker)Activator.CreateInstance(typeof(KCXGLHCPicker));
                case DataSource.ROBO:
                    return (IPicker)Activator.CreateInstance(typeof(RoboXGLHCPicker));
                default:
                    return null;
            }
        }
    }
}
