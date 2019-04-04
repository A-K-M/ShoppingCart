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
                            ProductId = (int)reader["ProductID"],
                            Quantity = (int)reader["Quantity"],
                        });
                    }
                }
            }
            return cart;
        }

        public static Product GetProductByProductId(int ProductId)
        {
            Product product = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string q = @"SELECT ProductId, ProductName, ProductDescription, UnitPrice, Image
                            from Products
                            WHERE ProductId = " + ProductId;

                SqlCommand cmd = new SqlCommand(q, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product = new Product()
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductName = (string)reader["ProductName"],
                        ProductDescription = (string)reader["ProductDescription"],
                        UnitPrice = (decimal)reader["UnitPrice"],
                        ImagePath = (string)reader["Image"]
                    };
                }
            }
            return product;
        }

        public static bool IsActiveCartId(int productId, string sessionId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT COUNT(*) FROM Customers, CartDetails
                    WHERE sessionId = '" + sessionId + "' AND CustomerId = CartId AND ProductId = " + productId;
                SqlCommand cmd = new SqlCommand(sql, conn);
                int count = (int)cmd.ExecuteScalar();
                return (count >= 1);
            }
        }

        public static void CreateCart(int CustomerId, int ProductId)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO CartDetails (CartId, ProductId, Quantity)
                            VALUES ( " + CustomerId + ", " + ProductId + ", 1)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
        public static void AddToCart(int ProductId, int CustomerId, string sessionId)
        {
            if (IsActiveCartId(ProductId, sessionId) == false)
                CreateCart(CustomerId, ProductId);
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"SELECT Quantity FROM CartDetails, Customers 
                                        WHERE SessionId = '" + sessionId + "' AND CartDetails.ProductId = " + ProductId;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    int quantity = (int)cmd.ExecuteScalar();
                    sql = @"UPDATE CartDetails SET Quantity = " + (quantity + 1) +
                                " WHERE CartId = '" + CustomerId + "' AND ProductId = " + ProductId;
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static int GetCartQuantity(string sessionId)
        {
            List<CartDetail> cart = CartData.GetCart(sessionId);

            int quantity = 0;
            foreach (var cartItem in cart)
            {
                quantity += cartItem.Quantity;

            }
            return quantity;
        }

        //public int GetCartQuantity(int CartId)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        string sql = @"SELECT COUNT(*) FROM CartDetails
        //            WHERE CartId = " + CartId;
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        int count = (int)cmd.ExecuteScalar();
        //        return count;
        //    }
        //}
    }
}