using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SqlJsonSupport.Controllers
{
    public static class DbDataReaderExtensions
    {
        public static T GetFieldJson<T>(this DbDataReader reader, int ordinal)
        {
            string value = reader.GetFieldValue<string>(ordinal);
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
