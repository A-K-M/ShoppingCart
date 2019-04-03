using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ShoppingCart.Models;

namespace ShoppingCart.Database
{
    public class CartData : Data
    {
        public static List<CartDetail> GetCart(string sessionId)
        {
            List<CartDetail> cart = new List<CartDetail>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT CustomerId, CartId, ProductId, Quantity from  Customers, CartDetails 
                WHERE CustomerId = CartId AND SessionId = '" + sessionId + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader == null)
                {
                    return null;
                }
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        cart.Add(new CartDetail()
                        {
                            CartId = (int)reader["CartId"],
                            ProductID = (int)reader["ProductID"],
                            Quantity = (int)reader["Quantity"]
                        });
                    }
                }
            }
            return cart;
        }
    }
}