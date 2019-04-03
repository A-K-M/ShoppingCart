using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ShoppingCart.Models;

namespace ShoppingCart.Database
{
    public class ProductData : Data
    {
        public List<Product> GetAllProducts()
        {
            List<Product> products_list = new List<Product>();

            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string sql = @"SELECT ProductId, ProductName, ProductDescription, UnitPrice, Image
                            from Products order by ProductId";
                SqlCommand cmd = new SqlCommand(sql, conn);



                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        products_list.Add(new Product()
                        {
                            ProductId = (int)reader["ProductId"],
                            ProductName = (string)reader["ProductName"],
                            ProductDescription = (string)reader["ProductDescription"],
                            UnitPrice = (decimal)reader["UnitPrice"],
                            ImagePath = (string)reader["Image"]
                        });
                    }
                }
            }
            return products_list;
        }

        public static bool IsActiveCartId(string sessionId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT COUNT(*) FROM Customers, CartDetails
                    WHERE sessionId = '" + sessionId + "' AND CustomerId = CartId" ;
                SqlCommand cmd = new SqlCommand(sql, conn);
                int count = (int)cmd.ExecuteScalar();
                return (count >= 1);
            }
        }

        public static void CreateCart(int CustomerId)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"UPDATE CartDetails SET CartId = '" + CustomerId + 
                        "' WHERE CartId =" + CustomerId + " SET";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
        public static void AddToCart(Product product, string sessionId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"UPDATE CartDetails SET Quantity =  
                    WHERE SessionId = '" + sessionId + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}