using Alpha.Collector.Model;
using System;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 北京快乐8采集器管理器
    /// </summary>
    public class BJKL8PickerManager : PickerManager
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
                    return (IPicker)Activator.CreateInstance(typeof(_168BJKL8Picker));
                case DataSource.KCZX:
                    return (IPicker)Activator.CreateInstance(typeof(KCBJKL8Picker));
                case DataSource.ROBO:
                    return (IPicker)Activator.CreateInstance(typeof(RoboBJKL8Picker));
                default:
                    return null;
            }
        }
    }
}
