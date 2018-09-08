using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Data;

namespace Alpha.Collector.Utils
{
    /// <summary>
    /// Json帮助类
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 将字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T JsonToEntity<T>(string value, params JsonConverter[] converters)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }

            if (converters != null && converters.Length > 0)
            {
                return JsonConvert.DeserializeObject<T>(value, converters);
            }
            else
            {
                Type type = typeof(T);


                if (type == typeof(DataSet))
                {
                    return JsonConvert.DeserializeObject<T>(value, new DataSetConverter());
                }
                else if (type == typeof(DataTable))
                {
                    return JsonConvert.DeserializeObject<T>(value, new DataTableConverter());
                }
                return JsonConvert.DeserializeObject<T>(value);
            }
        }
    }
}
