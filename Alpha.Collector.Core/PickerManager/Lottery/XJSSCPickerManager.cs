using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 新疆时时彩采集器管理器
    /// </summary>
    public class XJSSCPickerManager : PickerManager
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
                    return (IPicker)Activator.CreateInstance(typeof(_168XJSSCPicker));
                case DataSource.KCZX:
                    return (IPicker)Activator.CreateInstance(typeof(KCXJSSCPicker));
                case DataSource.CJW:
                    return (IPicker)Activator.CreateInstance(typeof(CJWXJSSCPicker));
                default:
                    return null;
            }
        }
    }
}
