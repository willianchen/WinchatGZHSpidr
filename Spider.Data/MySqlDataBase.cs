using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Spider.Data
{
    public class MySqlDataBase : IDataBase
    {
        private IDataOptionProvider dataOptionProvider { get; set; }

        private IDbConnection dbConnection { get; set; }

        public MySqlDataBase(IDataOptionProvider _dataOptionProvider)
        {
            dataOptionProvider = _dataOptionProvider;
        }

        public IDbConnection GetConnection()
        {
            if (dbConnection == null)
                return new SqlConnection(dataOptionProvider.GetDataOption().ConnectString));
            return dbConnection;
        }
    }
}
