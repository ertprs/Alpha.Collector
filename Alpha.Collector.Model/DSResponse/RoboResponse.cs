using System.Collections.Generic;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// Robo采集返回对象
    /// </summary>
    public class RoboResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 如果失败了，错误消息
        /// </summary>
        public string Massage { get; set; }

        /// <summary>
        /// 开奖结果列表
        /// </summary>
        public List<RoboResultModel> Data { get; set; }
    }

    /// <summary>
    /// Robo开奖结果的模型
    /// </summary>
    public class RoboResultModel
    {
        /// <summary>
        /// 彩种类型
        /// </summary>
        public string lotteryType { get; set; }

        /// <summary>
        /// 期数
        /// </summary>
        public string stage { get; set; }

        /// <summary>
        /// 格式化后的期数
        /// </summary>
        public string formatStage { get; set; }

        /// <summary>
        /// 开奖结果
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 六合彩开奖结果的生肖
        /// </summary>
        public string sxResult { get; set; }

        /// <summary>
        /// 开奖时间
        /// </summary>
        public string openDate { get; set; }
    }
}
