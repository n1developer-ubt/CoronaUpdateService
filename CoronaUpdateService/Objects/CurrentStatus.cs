using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaUpdateService.Objects
{
    public static class CurrentStatus
    {
        public static string ConnectionString = "user id=root;port=3306;server=127.0.0.1;password=;database=CoronaUpdate";
        public static bool IsAvailable = true;
        public static string TableName = "NewData";
    }
}
