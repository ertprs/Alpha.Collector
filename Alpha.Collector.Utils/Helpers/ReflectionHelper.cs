using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Alpha.Collector.Utils
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// 获取实现了某个接口的所有类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<Type> GetClasses<T>()
        {
            try
            {
                Type type = typeof(T);
                List<Type> typeList = AppDomain.CurrentDomain.GetAssemblies().ToList()
                    .SelectMany(a => a.GetTypes())
                    .Where(p => type.IsAssignableFrom(p) && p.Name != type.Name).ToList();

                return typeList;
            }
            catch (Exception ex)
            {
                return new List<Type>();
            }
        }

        /// <summary>
        /// 获取指定名称属性的值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="type">对象</param>
        /// <param name="propertyName">属性的名称</param>
        public static object GetPropertyValue(Type type, string propertyName)
        {
            PropertyInfo property = type.GetProperty(propertyName);
            if (property == null)
            {
                return null;
            }

            return property.GetValue(type, null);
        }
    }
}
