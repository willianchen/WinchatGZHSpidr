using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Data
{
    public interface IDataBaseFactory
    {
        IDataBase Create(IDataOptionProvider dataOptionProvider);

        IDataBase Create();
    }
}
