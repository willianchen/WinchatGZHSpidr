using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Data
{
    public class DefaultDataOptionProvider : IDataOptionProvider
    {
        public DataOption GetDataOption()
        {
            return new DataOption() { ConnectString = "Database=t_wechat;Data Source=127.0.0.1;Port=3306;User Id=root;Password=123456;Charset=utf8;TreatTinyAsBoolean=false;", databaseType = DatabaseType.MySql };
        }
    }
}
