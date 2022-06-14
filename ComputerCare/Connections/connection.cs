using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ComputerCare.Connections;
using System.Configuration;
using System.Data;

namespace ComputerCare.Connections
{
    class connection
    {
        public static string connectionString = ConfigurationSettings.AppSettings["ConnectionString"];
        public static SqlConnection con = new SqlConnection(connectionString);

        public SqlConnection openConnection()
        {
            if (con != null && con.State != ConnectionState.Open)
            {
                con.Open();
                return con;
            }
            else
            {
                return con;
            }
            return con;
        }

        public void closeConnection()
        {
            con.Close();
        }
    }
}
