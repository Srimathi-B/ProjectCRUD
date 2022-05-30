using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectCRUD
{
    internal class Database
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=.\\SQLExpress;Initial Catalog=ProjectDB;Integrated Security=True;";

            return new SqlConnection(connectionString);
        }
    }
}
