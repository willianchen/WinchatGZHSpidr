using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Http
{
    public static class EnsureUtil
    {
        /// <summary>
        /// 断言值不为NULL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        public static void NotNull<T>(T argument, string argumentName) where T : class
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);
        }

        /// <summary>
        /// 断言值不为NULL且不为空
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        public static void NotNullOrEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
                throw new ArgumentNullException(argumentName);
        }
    }
}