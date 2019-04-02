using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ShoppingCart.Models;

namespace ShoppingCart.Database
{
    public class CustomerData : Data
    {
        public static Customer GetCustomerByUsername(string username)
        {
            Customer customer = null;

            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string sql = @"SELECT CustomerId, Username, Password from Customer
                    WHERE Username = '" + username + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer = new Customer()
                    {
                        CustomerId = (int)reader["CustomerId"],
                        Password = (string)reader["Password"]
                    };
                }
            }
            return customer;
        }

        public static Customer GetCustomerBySessionId(string sessionId)
        {
            Customer customer = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string q = @"SELECT CustomerID, Lecturer.Firstname, Lecturer.Lastname, Practice.Name FROM
                        Lecturer, Practice
                            WHERE Lecturer.PracticeId = Practice.Id
                                AND Lecturer.SessionId = '" + sessionId + "'";

                SqlCommand cmd = new SqlCommand(q, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer = new Customer()
                    {
                        CustomerId = (int)reader["CustomerId"],
                    };
                }
            }
            }

            return customer;
        }
    }
}