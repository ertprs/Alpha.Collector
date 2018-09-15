using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Alpha.Collector.Core
{
    /// <summary>
    /// 彩种帮助类
    /// </summary>
    public static class LotteryHelper
    {
        /// <summary>
        /// 获取LotteryEnum中彩种的数量
        /// </summary>
        /// <returns></returns>
        public static List<Lottery> GetLotteryList()
        {
            Type type = typeof(LotteryEnum);
            List<FieldInfo> fieldInfo = type.GetFields().ToList();
            return (from f in fieldInfo
                    select new Lottery
                    {
                        create_time = DateTime.Now,
                        name = ModelFunction.GetLotteryName(f.Name),
                        code = f.Name
                    }).ToList();
        }

        /// <summary>
        /// 根据彩种名称获取采集器列表
        /// </summary>
        /// <param name="lottery"></param>
        public static List<IPicker> GetPickerList(string lottery)
        {
            List<Type> list = GetTypeList(lottery);
            if (list.Count == 0)
            {
                return new List<IPicker>();
            }

            List<IPicker> pickerList = new List<IPicker>();
            foreach (Type t in list)
            {
                try
                {
                    IPicker picker = Activator.CreateInstance(t) as IPicker;
                    if (picker != null)
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
        /// 根据彩种和数据源获取采集器
        /// </summary>
        /// <param name="lottery">彩种代码</param>
        /// <param name="dataSource">数据源代码</param>
        public static IPicker GetPicker(string lottery, string dataSource)
        {
            List<Type> list = GetTypeList(lottery);
            if (list.Count == 0)
            {
                return null;
            }
            
            list = list.Where(t => t.Name.Contains(dataSource)).ToList();
            if (list.Count == 0)
            {
                return null;
            }


            try
            {
                IPicker picker = Activator.CreateInstance(list[0]) as IPicker;
                return picker;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取彩种对象的接口
        /// </summary>
        /// <param name="lottery"></param>
        /// <returns></returns>
        public static List<Type> GetTypeList(string lottery)
        {
            switch (lottery)
            {
                case LotteryEnum.BJK3: return ReflectionHelper.GetClasses<IBJK3Picker>();
                case LotteryEnum.AHK3: return ReflectionHelper.GetClasses<IAHK3Picker>();
                case LotteryEnum.FJK3: return ReflectionHelper.GetClasses<IFJK3Picker>();
                case LotteryEnum.GSK3: return ReflectionHelper.GetClasses<IGSK3Picker>();
                case LotteryEnum.GXK3: return ReflectionHelper.GetClasses<IGXK3Picker>();
                case LotteryEnum.GZK3: return ReflectionHelper.GetClasses<IGZK3Picker>();
                case LotteryEnum.HeBK3: return ReflectionHelper.GetClasses<IHeBK3Picker>();
                case LotteryEnum.HuBK3: return ReflectionHelper.GetClasses<IHuBK3Picker>();
                case LotteryEnum.JLK3: return ReflectionHelper.GetClasses<IJLK3Picker>();
                case LotteryEnum.JSK3: return ReflectionHelper.GetClasses<IJSK3Picker>();
                case LotteryEnum.NMGK3: return ReflectionHelper.GetClasses<INMGK3Picker>();
                case LotteryEnum.SHK3: return ReflectionHelper.GetClasses<ISHK3Picker>();
                case LotteryEnum.AH11X5: return ReflectionHelper.GetClasses<IAH11X5Picker>();
                case LotteryEnum.GD11X5: return ReflectionHelper.GetClasses<IGD11X5Picker>();
                case LotteryEnum.GX11X5: return ReflectionHelper.GetClasses<IGX11X5Picker>();
                case LotteryEnum.HB11X5: return ReflectionHelper.GetClasses<IHB11X5Picker>();
                case LotteryEnum.JL11X5: return ReflectionHelper.GetClasses<IJL11X5Picker>();
                case LotteryEnum.JS11X5: return ReflectionHelper.GetClasses<IJS11X5Picker>();
                case LotteryEnum.JX11X5: return ReflectionHelper.GetClasses<IJX11X5Picker>();
                case LotteryEnum.LN11X5: return ReflectionHelper.GetClasses<ILN11X5Picker>();
                case LotteryEnum.NMG11X5: return ReflectionHelper.GetClasses<INMG11X5Picker>();
                case LotteryEnum.SD11X5: return ReflectionHelper.GetClasses<ISD11X5Picker>();
                case LotteryEnum.SH11X5: return ReflectionHelper.GetClasses<ISH11X5Picker>();
                case LotteryEnum.ZJ11X5: return ReflectionHelper.GetClasses<IZJ11X5Picker>();
                case LotteryEnum.CQXYNC: return ReflectionHelper.GetClasses<ICQXYNCPicker>();
                case LotteryEnum.GDKLSF: return ReflectionHelper.GetClasses<IGDKLSFPicker>();
                case LotteryEnum.TJKLSF: return ReflectionHelper.GetClasses<ITJKLSFPicker>();
                case LotteryEnum.HNKLSF: return ReflectionHelper.GetClasses<IHNKLSFPicker>();
                case LotteryEnum.GXKLSF: return ReflectionHelper.GetClasses<IGXKLSFPicker>();
                case LotteryEnum.CQSSC: return ReflectionHelper.GetClasses<ICQSSCPicker>();
                case LotteryEnum.TJSSC: return ReflectionHelper.GetClasses<ITJSSCPicker>();
                case LotteryEnum.XJSSC: return ReflectionHelper.GetClasses<IXJSSCPicker>();
                case LotteryEnum.FC3D: return ReflectionHelper.GetClasses<IFC3DPicker>();
                case LotteryEnum.TCPL3: return ReflectionHelper.GetClasses<ITCPL3Picker>();
                case LotteryEnum.PCDD: return ReflectionHelper.GetClasses<IPCDDPicker>();
                case LotteryEnum.BJKC: return ReflectionHelper.GetClasses<IBJKCPicker>();
                case LotteryEnum.BJKL8: return ReflectionHelper.GetClasses<IBJKL8Picker>();
                case LotteryEnum.XGLHC: return ReflectionHelper.GetClasses<IXGLHCPicker>();
                default: return null;
            }
        }
    }
}
