using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 重庆时时彩采集器管理器
    /// </summary>
    public class CQSSCPickerManager: PickerManager
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
                    return (IPicker)Activator.CreateInstance(typeof(_168CQSSCPicker));
                case DataSource.KCZX:
                    return (IPicker)Activator.CreateInstance(typeof(KCCQSSCPicker));
                case DataSource.CJW:
                    return (IPicker)Activator.CreateInstance(typeof(CJWCQSSCPicker));
                default:
                    return null;
            }
        }
    }
}
