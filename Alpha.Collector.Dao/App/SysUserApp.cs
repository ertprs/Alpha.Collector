using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;

namespace Alpha.Collector.Dao
{
    /// <summary>
    /// 用户App
    /// </summary>
    public class SysUserApp
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pagerInfo"></param>
        /// <returns></returns>
        public IList<SysUser> GetList(PagerInfo pagerInfo)
        {
            return MysqlHelper.GetListPagination<SysUser>(pagerInfo);
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysUser GetForm(string keyValue)
        {
            string sql = "select * from sys_user where id=@Id";
            return MysqlHelper.GetOne<SysUser>(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            //删除用户
            string sql = "delete from sys_user where id=@Id;";

            //删除用户登录信息
            sql += "delete from sys_userlogon where user_id=@Id;";
            MysqlHelper.ExcuteTransaction(sql, new { Id = keyValue });
        }

        /// <summary>
        /// 更新/新增实体
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userLogOn"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysUser model, SysUserLogOn userLogOn, string keyValue)
        {
            bool isAdd = string.IsNullOrEmpty(keyValue);
            string sql;
            if (!isAdd)
            {
                model.last_modify_time = DateTime.Now;
                model.last_modify_user = OperatorProvider.Provider.GetCurrent().UserId;
                sql = "update sys_user set account=@account, real_name=@real_name, nick_name=@nick_name, "
                    + "head_icon=@head_icon, gender=@gender, birthday=@birthday, "
                    + "mobile_phone=@mobile_phone, email=@email, web_chat=@web_chat, "
                    + "manage_id=@manage_id, security_level=@security_level, signature=@signature, "
                    + "organize_id=@organize_id, department_id=@department_id, role_id=@role_id, "
                    + "duty_id=@duty_id, is_administrator=@is_administrator, sort_code=@sort_code, "
                    + "enabled_mark=@enabled_mark, description=@description, "
                    + "last_modify_time=@last_modify_time, last_modify_user=@last_modify_user "
                    + "where id=@id";
                MysqlHelper.Execute(sql, model);
            }
            else
            {
                model.id = PublicFunction.GUID();
                model.create_time = DateTime.Now;
                model.create_user = OperatorProvider.Provider.GetCurrent().UserId;

                userLogOn.id = model.id;
                userLogOn.user_id = model.id;
                userLogOn.user_secretkey = Md5Helper.Md5(PublicFunction.CreateNo(), 16).ToLower();
                userLogOn.user_password = Md5Helper.Md5(DESHelper.Encrypt(Md5Helper.Md5(userLogOn.user_password, 32).ToLower(), userLogOn.user_secretkey).ToLower(), 32).ToLower();

                sql = "insert into sys_user (id, account, real_name, nick_name, head_icon, "
                    + "gender, birthday, mobile_phone, email, web_chat, manage_id, security_level, "
                    + "signature, organize_id, department_id, role_id, duty_id, is_administrator, "
                    + "sort_code, enabled_mark, description, create_time, create_user) values (@id, @account, @real_name, @nick_name, @head_icon, "
                    + "@gender, @birthday, @mobile_phone, @email, @web_chat, @manage_id, @security_level, "
                    + "@signature, @organize_id, @department_id, @role_id, @duty_id, @is_administrator, "
                    + "@sort_code, @enabled_mark, @description, @create_time, @create_user)";
                MysqlHelper.Execute(sql, model);

                sql = "insert into sys_userlogon (id, user_id, user_password, user_secretkey) "
                    + "values (@id, @user_id, @user_password, @user_secretkey)";
                MysqlHelper.Execute(sql, userLogOn);
            }
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="model"></param>
        public void UpdateForm(SysUser model)
        {
            string sql = "update sys_user set account=@account, real_name=@real_name, nick_name=@nick_name, "
                        + "head_icon=@head_icon, gender=@gender, birthday=@birthday, "
                        + "mobile_phone=@mobile_phone, email=@email, web_chat=@web_chat, "
                        + "manage_id=@manage_id, security_level=@security_level, signature@signature, "
                        + "organize_id=@organize_id, department_id=@department_id, role_id=@role_id, "
                        + "duty_id=@duty_id, is_administrator=@is_administrator, sort_code=@sort_code, "
                        + "enabled_mark=@enabled_mark, description=@description, "
                        + "last_modify_time=@last_modify_time, last_modify_user=@last_modify_user "
                        + "where id=@id";
            model.last_modify_time = DateTime.Now;
            model.last_modify_user = OperatorProvider.Provider.GetCurrent().UserId;
            MysqlHelper.Execute(sql, model);
        }

        /// <summary>
        /// 设置账户的状态
        /// </summary>
        /// <param name="user"></param>
        public void SetAccount(SysUser user)
        {
            string sql = "update sys_user set enabled_mark=@enabled_mark, "
                       + "last_modify_time=@last_modify_time, last_modify_user=@last_modify_user "
                       + "where id=@id";
            user.last_modify_time = DateTime.Now;
            user.last_modify_user = OperatorProvider.Provider.GetCurrent().UserId;
            MysqlHelper.Execute(sql, user);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SysUser CheckLogin(string username, string password)
        {
            string sql = "select * from sys_user where account=@UserName";
            SysUser user = MysqlHelper.GetOne<SysUser>(sql, new { UserName = username });
            if (user != null)
            {
                if (user.enabled_mark)
                {
                    sql = "select * from sys_userlogon where id=@Id";
                    SysUserLogOn userLogOn = MysqlHelper.GetOne<SysUserLogOn>(sql, new { Id = user.id });
                    if (userLogOn != null)
                    {
                        string dbPassword = Md5Helper.Md5(DESHelper.Encrypt(password.ToLower(), userLogOn.user_secretkey).ToLower(), 32).ToLower();
                        if (dbPassword == userLogOn.user_password)
                        {
                            DateTime time = new DateTime(1800, 1, 1);
                            userLogOn.previous_visit_time = userLogOn.last_visit_time < time ? time : userLogOn.last_visit_time.ToDate();

                            userLogOn.last_visit_time = DateTime.Now;
                            userLogOn.log_on_count = userLogOn.log_on_count + 1;

                            sql = "update sys_userlogon set F_PreviousVisitTime=@F_PreviousVisitTime, "
                                + "last_visit_time=@last_visit_time, log_on_count=@log_on_count where id=@id";
                            MysqlHelper.Execute(sql, userLogOn);

                            return user;
                        }
                        else
                        {
                            throw new Exception("密码不正确，请重新输入");
                        }
                    }
                    else
                    {
                        throw new Exception("账户不存在，请重新输入");
                    }
                }
                else
                {
                    throw new Exception("账户被系统锁定,请联系管理员");
                }
            }
            else
            {
                throw new Exception("账户不存在，请重新输入");
            }
        }
    }
}

