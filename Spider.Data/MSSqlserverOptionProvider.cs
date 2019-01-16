using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Data
{
    public class MSSqlserverOptionProvider : IDataOptionProvider
    {
        public DataOption GetDataOption()
        {
            return new DataOption()
            {
                databaseType = DatabaseType.MSSqlServer,
                ConnectString = "Min Pool Size=10;Max Pool Size=500;Connection Timeout=50;Data Source=127.0.0.1;Initial Catalog=Demo;Persist Security Info=True;User ID=sa;Password=123123"
            };
        }
    }
}
