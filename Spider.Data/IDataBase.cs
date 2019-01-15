using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Spider.Data
{
    public interface IDataBase
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        IDbConnection GetConnection();

    }
}
