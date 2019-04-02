﻿using System;
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
                        Id = (int)reader["CustomerId"],
                        Password = (string)reader["Password"]
                    };
                }
            }
            return customer;

        }
    }
}