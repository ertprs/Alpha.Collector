namespace Alpha.Collector.Model
{
    /// <summary>
    /// 模型扩展方法
    /// </summary>
    public static class ModelExts
    {
        /// <summary>
        /// 获取彩种名称
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string LotteryName(this OpenResult result)
        {
            return ModelFunction.GetLotteryName(result.lottery_code);
        }

        /// <summary>
        /// 彩种是否合法
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string IsLegal(this OpenResult result)
        {
            return result.is_legal == 1 ? "是" : "否";
        }
    }
}
