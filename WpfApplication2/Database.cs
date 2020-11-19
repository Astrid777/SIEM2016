using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finisar.SQLite;

namespace WpfApplication2
{
    class Database
    {
        public void SaveToDatabase(Log lg)
        {
            var log = new Log();

            //планировалось так(

            //sqlite_cmd.CommandText =
            //    "INSERT INTO E (System, User, GUID, CodeEvent, Domain, date, other) VALUES ('" + log.System +
            //    "', '" + log.User + "', '" + log.GuId + "', '" + log.CodeEvent + "', '" + log.Domain + "', '" +
            //    log.date + "', '" + log.other + "');";

            //sqlite_cmd.ExecuteNonQuery();
        }
    }
}