using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Data
{
    /// <summary>
    /// sql构造器
    /// </summary>
    public class SqlQuery
    {

        public string ComandText { get; set; }

        public object Parameters { get; set; }
    }
}
