using Alpha.Collector.Model;
using Alpha.Collector.Utils;

namespace Alpha.Collector.Dao
{
    public class SysUserLogOnApp
    {
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysUserLogOn GetForm(string keyValue)
        {
            string sql = "select * from sys_userlogon where id=@Id";
            return MysqlHelper.GetOne<SysUserLogOn>(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="model"></param>
        public void UpdateForm(SysUserLogOn model)
        {
            
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        public void RevisePassword(string userPassword, string keyValue)
        {
            SysUserLogOn userLogOn = new SysUserLogOn();
            userLogOn.id = keyValue;
            userLogOn.user_secretkey = Md5Helper.Md5(PublicFunction.CreateNo(), 16).ToLower();
            userLogOn.user_password = Md5Helper.Md5(DESHelper.Encrypt(Md5Helper.Md5(userPassword, 32).ToLower(), userLogOn.user_secretkey).ToLower(), 32).ToLower();

            string sql = "update sys_userlogon set user_secretkey=@user_secretkey, "
                       + "user_password=@user_password where id=@id";
            MysqlHelper.Execute(sql, userLogOn);
        }
    }
}
