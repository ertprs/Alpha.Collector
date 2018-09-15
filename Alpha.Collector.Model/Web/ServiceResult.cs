using System;
using System.Collections.Generic;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// ServiceResult
    /// </summary>
    public class ServiceResult
    {
        public ServiceResult()
        {

        }

        public ServiceResult(string msg)
        {
            Message = msg;
        }

        /// <summary>
        /// 结果码
        /// </summary>
        public int ResultCode;

        /// <summary>
        /// 消息
        /// </summary>
        public string Message;

        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception;

        /// <summary>
        /// 数据
        /// </summary>
        public Dictionary<string, object> Data = new Dictionary<string, object>();

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success => ResultCode > 0;

        /// <summary>
        /// 是否错误
        /// </summary>
        public bool Error => ResultCode <= 0;
    }
}
