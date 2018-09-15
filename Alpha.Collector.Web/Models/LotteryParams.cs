namespace Alpha.Collector.Web
{
    /// <summary>
    /// 彩种搜索条件
    /// </summary>
    public class LotteryParams
    {
        /// <summary>
        /// 彩种
        /// </summary>
        public string LotteryCode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }
    }
}