using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ShoppingCart.Database
{
    public class SessionData : Data
    {
        public static bool IsActiveSessionId(string sessionId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT COUNT(*) FROM Customer
                    WHERE sessionId = '" + sessionId + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                int count = (int)cmd.ExecuteScalar();
                return (count == 1);
            }
        }

        public static string CreateSession(int userId)
        {
            string sessionId = Guid.NewGuid().ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"UPDATE Customer SET SessionId = '" + sessionId + "'" +
                        " WHERE Id =" + userId;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }

            return sessionId;
        }

        public static void RemoveSession(string sessionId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"UPDATE Customer SET SessionId = NULL 
                    WHERE SessionId = '" + sessionId + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}