namespace Alpha.Collector.Model
{
    /// <summary>
    /// 抓取数量
    /// </summary>
    public class PickCount
    {
        /// <summary>
        /// 彩种代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 今日采集数
        /// </summary>
        public int TodayCount { get; set; }

        /// <summary>
        /// 总采集数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 采集器总数
        /// </summary>
        public int TotalPickerCount { get; set; }

        /// <summary>
        /// 有效的采集器数量
        /// </summary>
        public int ValidPickerCount { get; set; }
    }

    /// <summary>
    /// 彩种采集详情
    /// </summary>
    public class LotteryPickDetail
    {
        /// <summary>
        /// 数据源代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 数据源名称
        /// </summary>
        public string DataSourceName
        {
            get { return ModelFunction.GetDataSourceName(this.Code); }
        }

        /// <summary>
        /// 采集器名称
        /// </summary>
        public string PickerName { get; set; }

        /// <summary>
        /// 采集器是否可用
        /// </summary>
        public bool PickerValid { get; set; }

        /// <summary>
        /// 采集器状态文字
        /// </summary>
        public string PickerValidText
        {
            get { return this.PickerValid ? "抓取中" : "停止中"; }
        }

        /// <summary>
        /// 今日采集数
        /// </summary>
        public int TodayCount { get; set; }

        /// <summary>
        /// 总采集数
        /// </summary>
        public int TotalCount { get; set; }
    }

    /// <summary>
    /// 数据源采集详情
    /// </summary>
    public class DataSourcePickDetail
    {
        /// <summary>
        /// 彩种代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 彩种名称
        /// </summary>
        public string LotteryName
        {
            get { return ModelFunction.GetLotteryName(this.Code); }
        }

        /// <summary>
        /// 采集器名称
        /// </summary>
        public string PickerName { get; set; }

        /// <summary>
        /// 采集器是否可用
        /// </summary>
        public bool PickerValid { get; set; }

        /// <summary>
        /// 采集器状态文字
        /// </summary>
        public string PickerValidText
        {
            get { return this.PickerValid ? "抓取中" : "停止中"; }
        }

        /// <summary>
        /// 今日采集数
        /// </summary>
        public int TodayCount { get; set; }

        /// <summary>
        /// 总采集数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
