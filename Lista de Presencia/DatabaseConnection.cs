using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Lista_de_Presencia
{
    class DatabaseConnection
    {
        private static readonly string s_Server = "USUARIO-PC\\SQLEXPRESS";
        private static readonly string s_Database = "MALM";

        public static void OpenConnection(SqlConnection conn)
        {
            conn.ConnectionString = "Server="+s_Server+";Database="+s_Database+";Trusted_Connection=true";
            //Console.WriteLine("Connection string: " + conn.ConnectionString);
            conn.Open();
        }
    }
}
