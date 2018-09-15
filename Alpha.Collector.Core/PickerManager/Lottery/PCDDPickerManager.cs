using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// PC蛋蛋采集器管理器
    /// </summary>
    public class PCDDPickerManager : PickerManager
    {
        /// <summary>
        /// 获取采集器列表
        /// </summary>
        /// <returns></returns>
        public override List<IPicker> GetPickerList()
        {
            List<Type> list = ReflectionHelper.GetClasses<IPCDDPicker>();
            List<IPicker> pickerList = new List<IPicker>();
            foreach (Type type in list)
            {
                try
                {
                    IPCDDPicker picker = Activator.CreateInstance(type) as IPCDDPicker;
                    if (picker != null && picker.IsValid)
                    {
                        pickerList.Add(picker);
                    }

                }
                catch (Exception ex)
                {

                }
            }

            return pickerList;
        }

        /// <summary>
        /// 获取采集器
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public override IPicker GetPicker(string dataSource)
        {
            List<Type> list = ReflectionHelper.GetClasses<IPCDDPicker>();
            foreach (Type type in list)
            {
                try
                {
                    IPCDDPicker picker = Activator.CreateInstance(type) as IPCDDPicker;
                    if (picker != null && type.Name.ToLower().Contains(dataSource.ToLower()))
                    {
                        return picker;
                    }

                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return null;
        }
    }
}
