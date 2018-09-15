using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 安徽快3采集器管理器
    /// </summary>
    public class AHK3PickerManager : PickerManager
    {
        /// <summary>
        /// 获取采集器列表
        /// </summary>
        /// <returns></returns>
        public override List<IPicker> GetPickerList()
        {
            List<Type> list = ReflectionHelper.GetClasses<IAHK3Picker>();
            List<IPicker> pickerList = new List<IPicker>();
            foreach (Type type in list)
            {
                try
                {
                    IAHK3Picker picker = Activator.CreateInstance(type) as IAHK3Picker;
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
            List<Type> list = ReflectionHelper.GetClasses<IAHK3Picker>();
            foreach (Type type in list)
            {
                try
                {
                    IAHK3Picker picker = Activator.CreateInstance(type) as IAHK3Picker;
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
