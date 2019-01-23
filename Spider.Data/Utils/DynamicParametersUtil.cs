using Dapper;
using Spider.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Data.Utils
{
    public static class DynamicParametersUtil
    {
        public static DynamicParameters BuildDynamicParameters<T>(T entity, Type[] ignoreAttribute)
        {
            DynamicParameters parameters = new DynamicParameters();
            var properties = PropertyUtil.GetPropertyInfos(entity, null , ignoreAttribute);
            foreach (var p in properties)
            {
                parameters.Add(p.Name, p.GetValue(entity));
            }
            return parameters;
        }
    }
}
