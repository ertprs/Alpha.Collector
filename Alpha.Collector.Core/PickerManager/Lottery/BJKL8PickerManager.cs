using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 北京快乐8采集器管理器
    /// </summary>
    public class BJKL8PickerManager : PickerManager
    {
        /// <summary>
        /// 获取采集器列表
        /// </summary>
        /// <returns></returns>
        public override List<IPicker> GetPickerList()
        {
            List<Type> list = ReflectionHelper.GetClasses<IBJKL8Picker>();
            List<IPicker> pickerList = new List<IPicker>();
            foreach (Type type in list)
            {
                try
                {
                    IBJKL8Picker picker = Activator.CreateInstance(type) as IBJKL8Picker;
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
            List<Type> list = ReflectionHelper.GetClasses<IBJKL8Picker>();
            foreach (Type type in list)
            {
                try
                {
                    IBJKL8Picker picker = Activator.CreateInstance(type) as IBJKL8Picker;
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
