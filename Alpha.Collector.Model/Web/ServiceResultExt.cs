using System;

namespace Alpha.Collector.Model
{
    /// <summary>
    /// SeriviceResult扩展方法
    /// </summary>
    public static class ServiceResultExt
    {
        public static ServiceResult IsSucceed(this ServiceResult svr)
        {
            svr.ResultCode = 1;
            return svr;
        }

        public static ServiceResult IsSucceed(this ServiceResult svr, string msg)
        {
            svr.ResultCode = 1;
            svr.Message = msg;
            return svr;
        }

        public static ServiceResult IsSucceed(this ServiceResult svr, Exception ex)
        {
            svr.ResultCode = 1;
            SetMessage(svr, ex);
            return svr;
        }

        public static ServiceResult IsFailed(this ServiceResult svr)
        {
            svr.ResultCode = -1;
            return svr;
        }

        public static ServiceResult IsFailed(this ServiceResult svr, string msg)
        {
            svr.ResultCode = -1;
            svr.Message = msg;
            return svr;
        }

        public static ServiceResult IsFailed(this ServiceResult svr, Exception ex)
        {
            svr.ResultCode = -1;
            SetMessage(svr, ex);
            return svr;
        }

        public static ServiceResult IsErrored(this ServiceResult svr, Exception ex)
        {
            svr.ResultCode = -999;
            SetMessage(svr, ex);
            return svr;
        }

        private static void SetMessage(ServiceResult svr, Exception ex)
        {
            svr.Exception = ex;
            svr.Message = ((ex.InnerException == null) ? ex.ToString() : (ex.ToString() + "\r\nInnerException:\r\n" + ex.InnerException.ToString()));
        }

        public static void Set(this ServiceResult svr, string name, object value)
        {
            svr.Data[name] = value;
        }

        public static T Get<T>(this ServiceResult svr, string key) where T : class
        {
            if (svr.Data.ContainsKey(key))
            {
                return svr.Data[key] as T;
            }
            return null;
        }

        public static bool TryGet(this ServiceResult svr, string name, out object value)
        {
            return svr.Data.TryGetValue(name, out value);
        }
    }
}

