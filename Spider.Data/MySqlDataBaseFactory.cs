using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Data
{
    public class MySqlDataBaseFactory : IDataBaseFactory
    {
        public IDataBase Create(IDataOptionProvider dataOptionProvider)
        {
            return new MySqlDataBase(dataOptionProvider);
        }

        public IDataBase Create()
        {
            return new MySqlDataBase(new DefaultDataOptionProvider());
        }
    }
}
