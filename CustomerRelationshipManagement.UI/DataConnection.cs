using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CustomerRelationshipManagement.UI
{
    public class DataConnection
    {
        private SqlConnection connection = new SqlConnection(Helper.connVal("DBConnection"));

        public SqlConnection openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            return connection;
        }
        public SqlConnection closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
            return connection;
        }
    }
}
