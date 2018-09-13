namespace Alpha.Collector.Utils
{
    /// <summary>
    /// 登录用户类  单例模式
    /// </summary>
    public class OperatorProvider
    {
        /// <summary>
        /// 登录用户实例
        /// </summary>
        public static OperatorProvider Provider
        {
            get { return new OperatorProvider(); }
        }

        /// <summary>
        /// 登录用户的session或cookie键名称
        /// </summary>
        private string LoginUserKey = "robo_loginuserkey_2016";

        /// <summary>
        /// 登录提供者模式 Cookie或Session
        /// </summary>
        private string LoginProvider = ConfigHelper.GetValue("LoginProvider");

        /// <summary>
        /// 读取当前的登录用户
        /// </summary>
        /// <returns></returns>
        public OperatorModel GetCurrent()
        {
            return LoginProvider == "Cookie" ? DESHelper.Decrypt(WebHelper.GetCookie(LoginUserKey)).ToEntity<OperatorModel>() : DESHelper.Decrypt(WebHelper.GetSession(LoginUserKey).ToString()).ToEntity<OperatorModel>();
        }

        /// <summary>
        /// 设置当前登录用户  
        /// </summary>
        /// <param name="operatorModel"></param>
        public void AddCurrent(OperatorModel operatorModel)
        {
            if (LoginProvider == "Cookie")
            {
                WebHelper.WriteCookie(LoginUserKey, DESHelper.Encrypt(operatorModel.ToJson()), 60);
            }
            else
            {
                WebHelper.WriteSession(LoginUserKey, DESHelper.Encrypt(operatorModel.ToJson()));
            }
        }

        /// <summary>
        /// 移除当前登录用户
        /// </summary>
        public void RemoveCurrent()
        {
            if (LoginProvider == "Cookie")
            {
                WebHelper.RemoveCookie(LoginUserKey.Trim());
            }
            else
            {
                WebHelper.RemoveSession(LoginUserKey.Trim());
            }
        }
    }
}
