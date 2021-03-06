﻿using System;
using System.Collections.Generic;
using System.Linq;
using Spider.Infrastructure.Logs.Abstractions;

namespace Spider.Infrastructure.Logs.Extensions
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="log">日志操作</param>
        public static ILog Content(this ILog log)
        {
            return log.Set<ILogContent>(content => content.Content(""));
        }

        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="log">日志操作</param>
        /// <param name="value">值</param>
        public static ILog Content(this ILog log, string value)
        {
            return log.Set<ILogContent>(content => content.Content(value));
        }

        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="log">日志操作</param>
        /// <param name="dictionary">字典</param>
        public static ILog Content(this ILog log, IDictionary<string, object> dictionary)
        {
            if (dictionary == null)
                return log;
            return Content(log, dictionary.ToDictionary(t => t.Key, t => t.Value.ToString()));
        }

        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="log">日志操作</param>
        /// <param name="dictionary">字典</param>
        public static ILog Content(this ILog log, IDictionary<string, string> dictionary)
        {
            if (dictionary == null)
                return log;
            foreach (var keyValue in dictionary)
                log.Set<ILogContent>(content => content.Content($"{keyValue.Key} : {keyValue.Value}"));
            return log;
        }

        /// <summary>
        /// 设置错误内容
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static ILog Exception(this ILog log, Exception ex)
        {
            log.Set<ILogContent>(content => content.Content($"Message : { ex.Message} "));
            log.Set<ILogContent>(content => content.Content($"StackTrace : { ex.StackTrace} "));
            return log;
        }
    }
}
