using Spider.Data.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spider.Data.Utils
{
    public static class DatabaseUtil
    {
        /// <summary>
        /// 获取数据库名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetTableName<T>(T data)
        {
            string tableName = string.Empty;    //数据库名
            var sd = data.GetType().GetCustomAttributes(true);
            for (int i = 0; i < sd.Count(); i++)
            {
                if (sd.GetValue(i).GetType() == typeof(TableNameAttribute))
                {
                    TableNameAttribute tableNameTmp = sd[i] as TableNameAttribute;
                    tableName = tableNameTmp.tableName;
                }
            }
            return tableName;
        }
    }
}
