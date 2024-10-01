using System;
using System.Data;
using System.Data.SqlClient;

namespace User_Registration
{
    class DBcontext
    {
        string connString = @"Server=RIJAN;Database=Aliza_Database;Trusted_Connection=True";

        // Execute a SQL query (INSERT/UPDATE/DELETE) with parameters
        public void ExecuteSql(string query, Dictionary<string, object> parameters)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to the command
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery(); // Use ExecuteNonQuery for INSERT/UPDATE/DELETE
                    conn.Close();
                }
            }
        }

        // Get all data from a SELECT query
        public DataSet getAll(string query)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
        }
    }
}
