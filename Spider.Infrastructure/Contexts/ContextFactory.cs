using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Infrastructure.Contexts
{
    /// <summary>
    /// 上下文工厂
    /// </summary>
    public static class ContextFactory
    {
        /// <summary>
        /// 创建上下文
        /// </summary>
        public static IContext Create()
        {
            if (HttpContext.Current == null)
                return NullContext.Instance;
            return new WebContext();
        }
    }
}