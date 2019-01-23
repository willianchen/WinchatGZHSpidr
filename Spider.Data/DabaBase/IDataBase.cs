using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Data
{
    public interface IDataBase : IDisposable
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        IDbConnection GetConnection();

        int Execute(string sql, object param = null);

        int Execute(SqlQuery sqlQuery);

        Task<int> ExecuteAsync(string sql, object param = null);

        Task<int> ExecuteAsync(SqlQuery sqlQuery);

        Task<T> FirstOrDefaultAsync<T>(string sql, object param = null);

        Task<T> FirstOrDefaultAsync<T>(SqlQuery sqlQuery);

        T FirstOrDefault<T>(string sql, object param = null);

        T FirstOrDefault<T>(SqlQuery sqlQuery);

        Task<IEnumerable<T>> QueryListAsync<T>(string sql, object param = null);

        Task<IEnumerable<T>> QueryListAsync<T>(SqlQuery sqlQuery);

        IEnumerable<T> QueryList<T>(string sql, object param = null);

        IEnumerable<T> QueryList<T>(SqlQuery sqlQuery);

        int Insert<T>(T ob, string igoreFields);

    }
}
