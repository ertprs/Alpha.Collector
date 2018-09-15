using System;

namespace Alpha.Collector.Utils
{
    /// <summary>
    /// 异常扩展方法
    /// </summary>
    public static class ExceptionExt
    {
        /// <summary>
        /// 类为null时抛异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument"></param>
        /// <param name="paramName"></param>
        public static void ThrowIfNull<T>(this T argument, string paramName) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// 使用指定方式判断为false时抛异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument"></param>
        /// <param name="predicate"></param>
        /// <param name="msg"></param>
        public static void ThrowIf<T>(this T argument, Func<T, bool> predicate, string msg)
        {
            if (predicate(argument))
            {
                throw new ArgumentException(msg);
            }
        }
    }
}
