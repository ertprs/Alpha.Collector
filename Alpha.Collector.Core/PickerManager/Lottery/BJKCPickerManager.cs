using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 北京赛车采集器管理器
    /// </summary>
    public class BJKCPickerManager: PickerManager
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
                    return (IPicker)Activator.CreateInstance(typeof(_168BJKCPicker));
                case DataSource.KCZX:
                    return (IPicker)Activator.CreateInstance(typeof(KCBJKCPicker));
                case DataSource.ROBO:
                    return (IPicker)Activator.CreateInstance(typeof(RoboBJKCPicker));
                default:
                    return null;
            }
        }
    }
}
