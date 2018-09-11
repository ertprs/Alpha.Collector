using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 天津时时彩采集器管理器
    /// </summary>
    public class TJSSCPickerManager : PickerManager
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
                    return (IPicker)Activator.CreateInstance(typeof(_168TJSSCPicker));
                case DataSource.KCZX:
                    return (IPicker)Activator.CreateInstance(typeof(KCTJSSCPicker));
                case DataSource.CJW:
                    return (IPicker)Activator.CreateInstance(typeof(CJWTJSSCPicker));
                default:
                    return (IPicker)Activator.CreateInstance(typeof(_168TJSSCPicker));
            }
        }
    }
}
