using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NHibernate.Dialect;

namespace AptifyWebApi.Models.Shared
{
    public class MsSqlDialectExtend : MsSql2012Dialect
   
{
    protected override void RegisterLargeObjectTypeMappings()
    {
        base.RegisterLargeObjectTypeMappings();
        RegisterColumnType(DbType.Binary, 2147483647, "VARBINARY(MAX)");
    }
}  
}