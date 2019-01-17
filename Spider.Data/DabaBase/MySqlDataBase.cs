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
    public class MySqlDataBase : DataBase
    {
        public MySqlDataBase(IDataOptionProvider _dataOptionProvider) : base(_dataOptionProvider)
        {
        }
    }
}
