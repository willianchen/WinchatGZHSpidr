using Dapper;
using MySql.Data.MySqlClient;
using Spider.Data.Attribute;
using Spider.Data.Utils;
using Spider.Infrastructure.Extension;
using Spider.Infrastructure.Logs;
using Spider.Infrastructure.Logs.Extensions;
using Spider.Infrastructure.Logs.Nlog;
using Spider.Infrastructure.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Data
{
    public class DataBase : IDataBase
    {
        private IDataOptionProvider dataOptionProvider { get; set; }

        private ILog logger = NLogger.GetLog("DataBase");

        private DataOption option;
        /// <summary>
        /// 数据库访问单个请求单列
        /// </summary>
        private ConcurrentDictionary<string, IDbConnection> _dbConnectionCache = new ConcurrentDictionary<string, IDbConnection>();

        public DataBase(IDataOptionProvider _dataOptionProvider)
        {
            dataOptionProvider = _dataOptionProvider;
            option = dataOptionProvider.GetDataOption();
        }

        public IDbConnection GetConnection()
        {
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
                logger.Content($"执行Sql：{sqlQuery?.CommandText}");
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
                logger.Content($"执行Sql：{sqlQuery?.CommandText}");
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
                logger.Content($"执行Sql：{sqlQuery?.CommandText}");
                logger.Exception(ex);
                logger.Error();
                throw;
                throw;
            }
        }

        public int Insert<T>(T entity, string igoreFields)
        {
            var propertyList = GetInsertPropertyList(entity, null);
            var proNameList = propertyList.Select(m => m.Name);
            var columns = string.Join(',', proNameList);
            var values = string.Join(',', proNameList.Select(p => GetSign(option.databaseType) + p));
            var tableName = DatabaseUtil.GetTableName(entity);
            var keyName = PropertyUtil.GetAttributeProperty(entity, typeof(IdentityAttribute));
            string commandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2});{3};", tableName, columns, values, GetIdentityKeyScript(keyName, option.databaseType));
            var param = GetInsertDynamicParameters(entity);
            return GetConnection().ExecuteScalar<int>(commandText, param);
        }


        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public int Execute(SqlQuery sqlQuery)
        {
            return Monitor(() =>
            {
                return GetConnection().Execute(sqlQuery.CommandText, sqlQuery.Parameters, commandTimeout: sqlQuery.CommandTimeout, commandType: sqlQuery.CommandType);
            }, sqlQuery);

        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public Task<int> ExecuteAsync(SqlQuery sqlQuery)
        {
            return Monitor(() =>
            {
                return GetConnection().ExecuteAsync(sqlQuery.CommandText, sqlQuery.Parameters, commandTimeout: sqlQuery.CommandTimeout, commandType: sqlQuery.CommandType);
            }, sqlQuery);
        }

        /// <summary>
        /// 查询首个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public Task<T> FirstOrDefaultAsync<T>(SqlQuery sqlQuery)
        {
            return Monitor(() =>
            {
                return GetConnection().QueryFirstAsync<T>(sqlQuery.CommandText, sqlQuery.Parameters, commandTimeout: sqlQuery.CommandTimeout, commandType: sqlQuery.CommandType);
            }, sqlQuery);
        }

        /// <summary>
        /// 查询首个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public T FirstOrDefault<T>(SqlQuery sqlQuery)
        {
            return Monitor(() =>
            {
                return GetConnection().QueryFirst<T>(sqlQuery.CommandText, sqlQuery.Parameters, commandTimeout: sqlQuery.CommandTimeout, commandType: sqlQuery.CommandType);
            }, sqlQuery);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> QueryListAsync<T>(SqlQuery sqlQuery)
        {
            return Monitor(() =>
            {
                return GetConnection().QueryAsync<T>(sqlQuery.CommandText, sqlQuery.Parameters, commandTimeout: sqlQuery.CommandTimeout, commandType: sqlQuery.CommandType);
            }, sqlQuery);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryList<T>(SqlQuery sqlQuery)
        {
            return Monitor(() =>
            {
                return GetConnection().Query<T>(sqlQuery.CommandText, sqlQuery.Parameters, commandTimeout: sqlQuery.CommandTimeout, commandType: sqlQuery.CommandType);
            }, sqlQuery);
        }

        #region Common
        /// <summary>
        /// 获取自增的脚本语句
        /// </summary>
        /// <param name="keyName">主键名</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns>获取自增的脚本语句</returns>
        public static string GetIdentityKeyScript(string keyName, DatabaseType dbType)
        {
            string identityScript = string.Empty;
            switch (dbType)
            {
                case DatabaseType.MySql:
                    identityScript = " SELECT @@IDENTITY ";
                    break;

                case DatabaseType.MSSqlServer:
                    identityScript = " SELECT scope_identity() ";
                    break;

                    //case DatabaseType.PostgreSqlClient:
                    //    identityScript = string.Concat(" RETURNING ", keyName, " ");
                    //    break;
            }
            return identityScript;
        }

        /// 获取参数符号
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>参数符号</returns>
        public static string GetSign(DatabaseType dbType)
        {
            switch (dbType)
            {
                case DatabaseType.MSSqlServer:
                    return "@";

                case DatabaseType.MySql:
                    return "?";

                //case DatabaseType.Oracle:
                //    return ":";

                //case DatabaseType.PostgreSqlClient:
                //    return "@";

                default:
                    throw new NotSupportedException(dbType.ToString());
            }
        }

        /// <summary>
        /// 获取新增属性字段
        /// </summary>
        /// <typeparam name="T">字段类型</typeparam>
        /// <param name="entity"></param>
        /// <param name="ignoreFields">需要忽略的字段</param>
        /// <returns>新增属性字段</returns>
        private static IEnumerable<PropertyInfo> GetInsertPropertyList<T>(T entity, string[] ignoreFields = null)
        {
            return PropertyUtil.GetPropertyInfos(entity, ignoreFields, new Type[] { typeof(IdentityAttribute) });
        }

        /// <summary>
        /// 构架插入参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static DynamicParameters GetInsertDynamicParameters<T>(T entity)
        {
            return DynamicParametersUtil.BuildDynamicParameters(entity, new Type[] { typeof(IdentityAttribute) });
        }
        #endregion Common
    }
}
