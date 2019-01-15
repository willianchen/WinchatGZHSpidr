using System;
using System.Text;
using Spider.Infrastructure.Logs.Abstractions;
using Spider.Infrastructure.Logs.Nlog;

namespace Spider.Infrastructure.Logs.Nolg.Formats
{
    /// <summary>
    /// 内容格式化器
    /// </summary>
    public class ContentFormat : ILogFormat
    {
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="logContent">日志内容</param>
        public string Format(ILogContent logContent)
        {
            if (!(logContent is LogContent content))
                return string.Empty;
            return Format(content);
        }

        /// <summary>
        /// 内容格式化器实例
        /// </summary>
        public static readonly ILogFormat Instance = new ContentFormat();

        /// <summary>
        /// 格式化
        /// </summary>
        protected virtual string Format(LogContent content)
        {
            int line = 1;
            var result = new StringBuilder();
            Line1(result, content, ref line);
            Line2(result, content, ref line);
            Line3(result, content, ref line);
            Line4(result, content, ref line);
            Line5(result, content, ref line);
            Line6(result, content, ref line);
            Line7(result, content, ref line);
            Line8(result, content, ref line);
            Line9(result, content, ref line);
            Line10(result, content, ref line);
            Line11(result, content, ref line);
            Line12(result, content, ref line);
            Line13(result, content, ref line);
            Line14(result, content, ref line);
            Finish(result);
            return result.ToString();
        }

        /// <summary>
        /// 添加行
        /// </summary>
        protected void AppendLine(StringBuilder result, LogContent content, Action<StringBuilder, LogContent> action, ref int line)
        {
            Append(result, content, action, ref line);
            result.AppendLine();
        }

        /// <summary>
        /// 添加行
        /// </summary>
        protected void Append(StringBuilder result, LogContent content, Action<StringBuilder, LogContent> action, ref int line)
        {
            result.AppendFormat("{0}. ", line++);
            action(result, content);
        }

        /// <summary>
        /// 添加日志内容
        /// </summary>
        protected void Append(StringBuilder result, string caption, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;
            result.AppendFormat("{0}: {1}   ", caption, value);
        }

        /// <summary>
        /// 第1行
        /// </summary>
        protected void Line1(StringBuilder result, LogContent content, ref int line)
        {
            AppendLine(result, content, (r, c) =>
            {
                r.AppendFormat("{0}: {1} >> ", c.Level, c.LogName);
                r.AppendFormat("{0}: {1}   ", "追踪ID", c.TraceId);
                r.AppendFormat("{0}: {1}   ", "运行时间", c.OperationTime);
                if (string.IsNullOrWhiteSpace(c.Duration))
                    return;
                r.AppendFormat("{0}: {1} ", "执行时间", c.Duration);
            }, ref line);
        }

        /// <summary>
        /// 第2行
        /// </summary>
        protected void Line2(StringBuilder result, LogContent content, ref int line)
        {
            AppendLine(result, content, (r, c) =>
            {
                Append(r, "Ip", c.Ip);
                Append(r, "Host", c.Host);
                Append(r, "ThreadId", c.ThreadId);
            }, ref line);
        }

        /// <summary>
        /// 第3行
        /// </summary>
        protected void Line3(StringBuilder result, LogContent content, ref int line)
        {
            if (string.IsNullOrWhiteSpace(content.Browser))
                return;
            AppendLine(result, content, (r, c) => Append(r, "浏览器", c.Browser), ref line);
        }

        /// <summary>
        /// 第4行
        /// </summary>
        protected void Line4(StringBuilder result, LogContent content, ref int line)
        {
            if (string.IsNullOrWhiteSpace(content.Url))
                return;
            AppendLine(result, content, (r, c) => r.Append("Url: " + c.Url), ref line);
        }

        /// <summary>
        /// 第5行
        /// </summary>
        protected void Line5(StringBuilder result, LogContent content, ref int line)
        {
            if (string.IsNullOrWhiteSpace(content.UserId) && string.IsNullOrWhiteSpace(content.Operator)
                && string.IsNullOrWhiteSpace(content.Role))
                return;
            AppendLine(result, content, (r, c) =>
            {
                Append(r, "操作人员编号", c.UserId);
                Append(r, "操作人", c.Operator);
                Append(r, "角色", c.Role);
            }, ref line);
        }

        /// <summary>
        /// 第6行
        /// </summary>
        protected void Line6(StringBuilder result, LogContent content, ref int line)
        {
            if (string.IsNullOrWhiteSpace(content.BusinessId) && string.IsNullOrWhiteSpace(content.Tenant)
                 && string.IsNullOrWhiteSpace(content.Application) && string.IsNullOrWhiteSpace(content.Module))
                return;
            AppendLine(result, content, (r, c) =>
            {
                Append(r, "业务编号", c.BusinessId);
                Append(r, "租户编号", c.Tenant);
                Append(r, "应用程序", c.Application);
                Append(r, "模块", c.Module);
            }, ref line);
        }

        /// <summary>
        /// 第7行
        /// </summary>
        protected void Line7(StringBuilder result, LogContent content, ref int line)
        {
            if (string.IsNullOrWhiteSpace(content.Class) && string.IsNullOrWhiteSpace(content.Method))
                return;
            AppendLine(result, content, (r, c) =>
            {
                Append(r, "类名", c.Class);
                Append(r, "方法", c.Method);
            }, ref line);
        }

        /// <summary>
        /// 第8行
        /// </summary>
        protected void Line8(StringBuilder result, LogContent content, ref int line)
        {
            if (content.Params.Length == 0)
                return;
            Append(result, content, (r, c) =>
            {
                r.AppendLine($"参数:");
                r.Append(c.Params);
            }, ref line);
        }

        /// <summary>
        /// 第9行
        /// </summary>
        protected void Line9(StringBuilder result, LogContent content, ref int line)
        {
            //if (string.IsNullOrWhiteSpace(content.Caption))
            //    return;
            //AppendLine(result, content, (r, c) =>
            //{
            //    r.AppendFormat("{0}: {1}", LogResource.Caption, c.Caption);
            //}, ref line);
        }

        /// <summary>
        /// 第10行
        /// </summary>
        protected void Line10(StringBuilder result, LogContent content, ref int line)
        {
            if (content.Content.Length == 0)
                return;
            Append(result, content, (r, c) =>
            {
                r.AppendLine($"内容:");
                r.Append(c.Content);
            }, ref line);
        }

        /// <summary>
        /// 第11行
        /// </summary>
        protected void Line11(StringBuilder result, LogContent content, ref int line)
        {
            if (content.Sql.Length == 0)
                return;
            Append(result, content, (r, c) =>
            {
                r.AppendLine($"Sql语句:");
                r.Append(c.Sql);
            }, ref line);
        }

        /// <summary>
        /// 第12行
        /// </summary>
        protected void Line12(StringBuilder result, LogContent content, ref int line)
        {
            if (content.SqlParams.Length == 0)
                return;
            Append(result, content, (r, c) =>
            {
                r.AppendLine($"Sql参数:");
                r.Append(c.SqlParams);
            }, ref line);
        }

        /// <summary>
        /// 第13行
        /// </summary>
        protected void Line13(StringBuilder result, LogContent content, ref int line)
        {
            //if (content.Exception == null)
            //    return;
            //AppendLine(result, content, (r, c) =>
            //{
            //    r.AppendLine($"{LogResource.Exception}: { GetErrorCode(content.ErrorCode) }");
            //    r.Append($"   { c.Exception.Message }");
            //}, ref line);
        }

        /// <summary>
        /// 获取错误码
        /// </summary>
        private string GetErrorCode(string errorCode)
        {
            if (string.IsNullOrWhiteSpace(errorCode))
                return string.Empty;
            return $"-- ErrorCode: {errorCode}";
        }

        /// <summary>
        /// 第14行
        /// </summary>
        protected void Line14(StringBuilder result, LogContent content, ref int line)
        {
            //if (content.Exception == null)
            //    return;
            //AppendLine(result, content, (r, c) =>
            //{
            //    r.AppendLine($"{LogResource.StackTrace}:");
            //    r.Append(c.Exception.StackTrace);
            //}, ref line);
        }

        /// <summary>
        /// 结束
        /// </summary>
        protected void Finish(StringBuilder result)
        {
            for (int i = 0; i < 125; i++)
                result.Append("-");
        }
    }
}
