using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Data
{
    public class DataBase : IDataBase
    {
        private IDataOptionProvider dataOptionProvider { get; set; }

        private IDbConnection dbConnection { get; set; }

        public DataBase(IDataOptionProvider _dataOptionProvider)
        {
            dataOptionProvider = _dataOptionProvider;
        }

        public IDbConnection GetConnection()
        {
            var option = dataOptionProvider.GetDataOption();

            if (dbConnection == null)
            {
                if (option.databaseType == DatabaseType.MySql)
                    dbConnection = new MySqlConnection(dataOptionProvider.GetDataOption().ConnectString);
                else if (option.databaseType == DatabaseType.MSSqlServer)
                    dbConnection = new SqlConnection(dataOptionProvider.GetDataOption().ConnectString);
            }
            return dbConnection;
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Execute(string sql, object param = null)
        {
            return GetConnection().Execute(sql);
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<int> ExecuteAsync(string sql, object param = null)
        {
            return GetConnection().ExecuteAsync(sql, param);
        }

        public Task<T> FirstOrDefaultAsync<T>(string sql, object param = null)
        {
            return GetConnection().QueryFirstOrDefaultAsync<T>(sql, param);
        }


        public T FirstOrDefault<T>(string sql, object param = null)
        {
            return GetConnection().QueryFirstOrDefault<T>(sql, param);
        }

        public Task<IEnumerable<T>> QueryListAsync<T>(string sql, object param = null)
        {
            return GetConnection().QueryAsync<T>(sql, param);
        }

        public IEnumerable<T> QueryList<T>(string sql, object param = null)
        {
            return GetConnection().Query<T>(sql, param);
        }


    }
}
