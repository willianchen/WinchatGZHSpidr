using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Data.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class TableNameAttribute : System.Attribute
    {
        public string tableName;

        public TableNameAttribute(string _tableName)
        {
            this.tableName = _tableName;
        }
   
    }
}
