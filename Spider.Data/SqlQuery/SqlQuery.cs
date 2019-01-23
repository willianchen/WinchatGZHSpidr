using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Dapper;

namespace Spider.Data
{
    /// <summary>
    /// sql构造器
    /// </summary>
    public class SqlQuery
    {
        /// <summary>
        /// 默认超时
        /// </summary>
        protected const int COMMAND_TIMEOUT_DEFAULT = 30000;

        /// <summary>
        /// 文本
        /// </summary>
        protected string _commandText;

        /// <summary>
        /// 超时
        /// </summary>
        protected int _commandTimeout = 30000;

        /// <summary>
        /// 文本类型
        /// </summary>
        protected CommandType _commandType = CommandType.Text;

        /// <summary>
        /// 执行SQL文本
        /// </summary>
        private string _comandText { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        private DynamicParameters _parameters { get; set; }

        public SqlQuery(string CommandText, object para = null)
        {
            _comandText = CommandText;
            _parameters = new DynamicParameters();
            if (para != null)
                _parameters.AddDynamicParams(para);
        }

        /// <summary>
        /// 执行文本
        /// </summary>
        public virtual string CommandText
        {
            get
            {
                return _commandText;
            }
            set
            {
                _commandText = value;
            }
        }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int CommandTimeout
        {
            get
            {
                return this._commandTimeout;
            }
            set
            {
                if (value < 10)
                {
                    this._commandTimeout = 300;
                    return;
                }
                this._commandTimeout = value;
            }
        }

        /// <summary>
        /// 指定如何解释命令字符串
        /// </summary>
        public CommandType CommandType
        {
            get
            {
                return _commandType;
            }
            set
            {
                this._commandType = value;
            }
        }

        /// <summary>
        /// 参数值（用于和数据库交互）
        /// </summary>
        public DynamicParameters Parameters
        {
            get
            {
                return _parameters;
            }
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        public SqlQuery AddParameter(string parameterName, object value, DbType? dbType)
        {
            Parameters.Add(parameterName, value, dbType);
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="size">长度</param>
        public SqlQuery AddParameter(string parameterName, object value, DbType? dbType, int? size)
        {
            Parameters.Add(parameterName, value, dbType, size: size);
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="size">长度</param>
        /// <param name="direction">Dataset参数类型</param>
        /// <param name="scale">参数值的精度</param>
        public SqlQuery AddParameter(string parameterName, object value, DbType? dbType, int? size, ParameterDirection? direction, byte? scale = null)
        {
            Parameters.Add(parameterName, value, dbType, direction, size: size, scale: scale);
            return this;
        }
    }
}
