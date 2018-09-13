﻿using System.Configuration;
using System.Web;
using System.Xml;

namespace Alpha.Collector.Utils
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key"></param>
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString().Trim();
        }

        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="key">要修改的Key</param>
        /// <param name="value">要修改为的值</param>
        public static void SetValue(string key, string value)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(HttpContext.Current.Server.MapPath("~/Configs/system.config"));

            XmlNode xNode;
            XmlElement xElem1;
            XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//appSettings");

            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + key + "']");
            if (xElem1 != null)
            {
                xElem1.SetAttribute("value", value);
            }
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", key);
                xElem2.SetAttribute("value", value);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(HttpContext.Current.Server.MapPath("~/Configs/system.config"));
        }
    }
}
