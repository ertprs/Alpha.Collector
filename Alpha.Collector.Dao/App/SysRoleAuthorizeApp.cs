/*******************************************************************************
 * Copyright © 2018 Robo 版权所有
 * Author: Tony Stack


*********************************************************************************/

using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Collector.Dao
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public class SysRoleAuthorizeApp
    {
        private SysModuleApp moduleApp = new SysModuleApp();
        private SysModuleButtonApp moduleButtonApp = new SysModuleButtonApp();

        /// <summary>
        /// 取实体列表
        /// </summary>
        /// <param name="ObjectId"></param>
        /// <returns></returns>
        public IList<SysRoleAuthorize> GetList(string ObjectId)
        {
            string sql = "select * from sys_roleauthorize where object_id=@ObjectId";
            return MysqlHelper.GetList<SysRoleAuthorize>(sql, new { ObjectId = ObjectId });
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<SysModule> GetMenuList(string roleId)
        {
            List<SysModule> moduleList = new List<SysModule>();
            if (OperatorProvider.Provider.GetCurrent().IsAdmin)//系统管理员具有所有权限
            {
                moduleList = moduleApp.GetList().ToList();
            }
            else
            {
                List<SysModule> moduleData = moduleApp.GetList().ToList();

                string sql = "select * from sys_roleauthorize where object_id=@RoleId and item_type=1";
                var authorizeData = MysqlHelper.GetList<SysRoleAuthorize>(sql, new { RoleId = roleId });

                foreach (var item in authorizeData)
                {
                    SysModule module = moduleData.Find(t => t.id == item.item_id);
                    if (module != null)
                    {
                        moduleList.Add(module);
                    }
                }
            }
            return moduleList.OrderBy(t => t.sort_code).ToList();
        }

        /// <summary>
        /// 获取按钮列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<SysModuleButton> GetButtonList(string roleId)
        {
            var data = new List<SysModuleButton>();
            if (OperatorProvider.Provider.GetCurrent().IsAdmin)
            {
                data = moduleButtonApp.GetList().ToList();
            }
            else
            {
                var buttonData = moduleButtonApp.GetList().ToList();

                string sql = "select * from sys_roleauthorize where object_id=@RoleId and item_type=2";
                var authorizeData = MysqlHelper.GetList<SysRoleAuthorize>(sql, new { RoleId = roleId });

                foreach (var item in authorizeData)
                {
                    SysModuleButton moduleButtonEntity = buttonData.Find(t => t.id == item.item_id);
                    if (moduleButtonEntity != null)
                    {
                        data.Add(moduleButtonEntity);
                    }
                }
            }
            return data.OrderBy(t => t.sort_code).ToList();
        }

        /// <summary>
        /// 权限校验
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ActionValidate(string roleId, string moduleId, string action)
        {
            var authorizeurldata = new List<AuthorizeActionModel>();
            var cachedata = CacheFactory.CreateCacheHelper().GetCache<List<AuthorizeActionModel>>("authorizeurldata_" + roleId);
            if (cachedata == null)
            {
                var moduledata = moduleApp.GetList().ToList();
                var buttondata = moduleButtonApp.GetList().ToList();

                string sql = "select * from sys_roleauthorize where object_id=@RoleId";
                var authorizeData = MysqlHelper.GetList<SysRoleAuthorize>(sql, new { RoleId = roleId });

                foreach (var item in authorizeData)
                {
                    if (item.item_type == 1)
                    {
                        SysModule moduleEntity = moduledata.Find(t => t.id == item.item_id);
                        authorizeurldata.Add(new AuthorizeActionModel { id = moduleEntity.id, url_address = moduleEntity.url_address });
                    }
                    else if (item.item_type == 2)
                    {
                        SysModuleButton moduleButtonEntity = buttondata.Find(t => t.id == item.item_id);
                        authorizeurldata.Add(new AuthorizeActionModel { id = moduleButtonEntity.module_id, url_address = moduleButtonEntity.url_address });
                    }
                }
                CacheFactory.CreateCacheHelper().WriteCache(authorizeurldata, "authorizeurldata_" + roleId, DateTime.Now.AddMinutes(5));
            }
            else
            {
                authorizeurldata = cachedata;
            }

            authorizeurldata = authorizeurldata.FindAll(t => t.id.Equals(moduleId));
            foreach (var item in authorizeurldata)
            {
                if (!string.IsNullOrEmpty(item.url_address))
                {
                    string[] url = item.url_address.Split('?');
                    if (item.id == moduleId && url[0] == action)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
