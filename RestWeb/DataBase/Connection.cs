using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestWeb.DataBase
{
    public class Connection
    {
        public LLXCORE.MySQL.Connection connection { get; }

        public LLXCORE.MySQL.Adapter Adapter { get; }


        public Connection()
        {
            this.connection = new LLXCORE.MySQL.Connection()
            {
                ServerURL = "25.82.137.232",
                ServerPort = 3306,
                ServerDataBase = "wcfdbserver",
                ServerUserName = "WCFDB",
                ServerPassword = "test"
            };
            Adapter = new LLXCORE.MySQL.Adapter(connection);
        }

    }
}