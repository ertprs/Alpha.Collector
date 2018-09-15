using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 福彩3D采集器管理器
    /// </summary>
    public class FC3DPickerManager : PickerManager
    {
        /// <summary>
        /// 获取采集器列表
        /// </summary>
        /// <returns></returns>
        public override List<IPicker> GetPickerList()
        {
            List<Type> list = ReflectionHelper.GetClasses<IFC3DPicker>();
            List<IPicker> pickerList = new List<IPicker>();
            foreach (Type type in list)
            {
                try
                {
                    IFC3DPicker picker = Activator.CreateInstance(type) as IFC3DPicker;
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
            List<Type> list = ReflectionHelper.GetClasses<IFC3DPicker>();
            foreach (Type type in list)
            {
                try
                {
                    IFC3DPicker picker = Activator.CreateInstance(type) as IFC3DPicker;
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
