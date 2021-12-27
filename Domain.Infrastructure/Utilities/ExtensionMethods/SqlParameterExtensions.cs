using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Domain.Infrastructure.Utilities.ExtensionMethods
{
    public static class SqlParameterExtensions
    {
        public static void SqlParamterArray<T>(List<T> parameterValues, out List<SqlParameter> sqlParameters, out string prmString)
        { 
            sqlParameters = parameterValues.Select((id, index) => new SqlParameter($"@p{Guid.NewGuid().ToString().Replace("-","")}", id)).ToList();
            prmString = string.Join(", ", sqlParameters.Select(p => p.ParameterName));
        }
    }
}
