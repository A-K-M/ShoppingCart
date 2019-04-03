
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ShoppingCart.Models;

namespace ShoppingCart.Database
{
    public class PurchaseData
    {
        public static List<Purchase> GetPurchaseBySessionId(string sessionId)
        {
            List<Purchase> Purchase = new List<Purchase>();
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string sql = @"SELECT PurchaseId,Customers.CustomerId,OrderDate from Purchases,Customers
                             where Customers.CustomerId=Purchases.CustomerId 
                             AND sessionId= '" + sessionId + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Purchase _Purchase = new Purchase()
                    {
                        PurchaseId = (int)reader["PurchaseId"],
                        CustomerId = (int)reader["CustomerId"],

                        OrderDate = (DateTime)reader["OrderDate"]
                    };

                    Purchase.Add(_Purchase);
                }
            }
            return Purchase;
        }

    }
    }