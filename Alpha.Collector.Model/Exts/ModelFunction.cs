namespace Alpha.Collector.Model
{
    /// <summary>
    /// 模型方法
    /// </summary>
    public static class ModelFunction
    {
        /// <summary>
        /// 根据彩种代号获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetLotteryName(string code)
        {
            switch (code)
            {
                case LotteryEnum.AHK3: return "安徽快3";
                case LotteryEnum.BJK3: return "北京快3";
                case LotteryEnum.FJK3: return "福建快3";
                case LotteryEnum.GSK3: return "甘肃快3";
                case LotteryEnum.GXK3: return "广西快3";
                case LotteryEnum.GZK3: return "贵州快3";
                case LotteryEnum.HeBK3: return "河北快3";
                case LotteryEnum.HuBK3: return "湖北快3";
                case LotteryEnum.JLK3: return "吉林快3";
                case LotteryEnum.JSK3: return "江苏快3";
                case LotteryEnum.NMGK3: return "内蒙古快3";
                case LotteryEnum.SHK3: return "上海快3";
                case LotteryEnum.AH11X5: return "安徽11选5";
                case LotteryEnum.GD11X5: return "广东11选5";
                case LotteryEnum.GX11X5: return "广西11选5";
                case LotteryEnum.HB11X5: return "湖北11选5";
                case LotteryEnum.JL11X5: return "吉林11选5";
                case LotteryEnum.JS11X5: return "江苏11选5";
                case LotteryEnum.JX11X5: return "江西11选5";
                case LotteryEnum.LN11X5: return "辽宁11选5";
                case LotteryEnum.NMG11X5: return "内蒙古11选5";
                case LotteryEnum.SD11X5: return "山东11选5";
                case LotteryEnum.SH11X5: return "上海11选5";
                case LotteryEnum.ZJ11X5: return "浙江11选5";
                case LotteryEnum.CQXYNC: return "重庆幸运农场";
                case LotteryEnum.GDKLSF: return "广东快乐十分";
                case LotteryEnum.TJKLSF: return "天津快乐十分";
                case LotteryEnum.HNKLSF: return "湖南快乐十分";
                case LotteryEnum.GXKLSF: return "广西快乐十分";
                case LotteryEnum.CQSSC: return "重庆时时彩";
                case LotteryEnum.TJSSC: return "天津时时彩";
                case LotteryEnum.XJSSC: return "新疆时时彩";
                case LotteryEnum.FC3D: return "福彩3D";
                case LotteryEnum.TCPL3: return "体彩排列3";
                case LotteryEnum.PCDD: return "PC蛋蛋";
                case LotteryEnum.BJKC: return "北京PK10";
                case LotteryEnum.BJKL8: return "北京快乐8";
                case LotteryEnum.XGLHC: return "香港六合彩";
                default: return "未知";
            }
        }

        /// <summary>
        /// 根据数据源名称获取数据源代码
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetDataSourceName(string code)
        {
            switch (code)
            {
                case DataSourceEnum.BiFa: return "必发";
                case DataSourceEnum.CJW: return "彩经网";
                case DataSourceEnum.CSJ: return "彩世界";
                case DataSourceEnum.DuoBao: return "多宝";
                case DataSourceEnum.KC: return "快彩在线";
                case DataSourceEnum.Robo: return "乐博";
                case DataSourceEnum._168: return "168开奖网";
                default: return "未知";
            }
        }
    }
}
