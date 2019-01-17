using Dapper;
using MySql.Data.MySqlClient;
using Spider.Infrastructure.Extension;
using Spider.Infrastructure.Logs;
using Spider.Infrastructure.Logs.Extensions;
using Spider.Infrastructure.Logs.Nlog;
using System;
using System.Collections.Concurrent;
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

        private ILog logger = NLogger.GetLog("DataBase");

        /// <summary>
        /// 数据库访问单个请求单列
        /// </summary>
        private ConcurrentDictionary<string, IDbConnection> _dbConnectionCache = new ConcurrentDictionary<string, IDbConnection>();

        public DataBase(IDataOptionProvider _dataOptionProvider)
        {
            dataOptionProvider = _dataOptionProvider;
        }

        public IDbConnection GetConnection()
        {
            var option = dataOptionProvider.GetDataOption();
            return CreateConnection(option);
        }

        private IDbConnection CreateConnection(DataOption option)
        {
            IDbConnection dbConnection = null;
            if (option.databaseType == DatabaseType.MySql)
                dbConnection = new MySqlConnection(dataOptionProvider.GetDataOption().ConnectString);
            else if (option.databaseType == DatabaseType.MSSqlServer)
                dbConnection = new SqlConnection(dataOptionProvider.GetDataOption().ConnectString);
            return dbConnection;
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Execute(string sql, object param = null)
        {
            return Monitor(() =>
            {
                return GetConnection().Execute(sql);
            });
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            return await MonitorAsync(async () =>
            {
                return await GetConnection().ExecuteAsync(sql, param);
            });
        }

        public async Task<T> FirstOrDefaultAsync<T>(string sql, object param = null)
        {
            return await MonitorAsync(async () =>
            {
                return await GetConnection().QueryFirstOrDefaultAsync<T>(sql, param);
            });
        }


        public T FirstOrDefault<T>(string sql, object param = null)
        {
            return Monitor<T>(() =>
            {
                return GetConnection().QueryFirstOrDefault<T>(sql, param);
            });


        }

        public async Task<IEnumerable<T>> QueryListAsync<T>(string sql, object param = null)
        {
            return await MonitorAsync(async () =>
           {
               return await GetConnection().QueryAsync<T>(sql, param);
           });
        }



        public IEnumerable<T> QueryList<T>(string sql, object param = null)
        {
            return Monitor<IEnumerable<T>>(() =>
            {
                return GetConnection().Query<T>(sql, param);
            });
        }

        public void Dispose()
        {
            foreach (var item in _dbConnectionCache)
            {
                item.Value?.Close();
                item.Value?.Dispose();
            }
            _dbConnectionCache?.Clear();
        }

        /// <summary>
        /// 监控
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public T Monitor<T>(Func<T> action, SqlQuery sqlQuery = null)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                logger.Content($"执行Sql：{sqlQuery?.ComandText}");
                logger.Exception(ex);
                logger.Error();
                throw;
            }
        }

        /// <summary>
        /// 监控消耗时间
        /// </summary>
        public async Task MonitorAsync(Func<Task> action, SqlQuery sqlQuery = null)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                logger.Content($"执行Sql：{sqlQuery?.ComandText}");
                logger.Exception(ex);
                logger.Error();
                throw;
            }
        }

        public async Task<T> MonitorAsync<T>(Func<Task<T>> action, SqlQuery sqlQuery = null)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                logger.Content($"执行Sql：{sqlQuery?.ComandText}");
                logger.Exception(ex);
                logger.Error();
                throw;
                throw;
            }
        }
    }
}
