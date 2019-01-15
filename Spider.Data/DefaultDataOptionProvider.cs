using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Data
{
    public class DefaultDataOptionProvider : IDataOptionProvider
    {
        public DataOption GetDataOption()
        {
            return new DataOption() { ConnectString = "", databaseType = DatabaseType.MySql };
        }
    }
}
