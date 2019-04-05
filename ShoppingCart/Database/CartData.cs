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
        public static List<CartDetail> GetCart(int customerId)
        {
            List<CartDetail> cart = new List<CartDetail>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT CartId, ProductId, Quantity from CartDetails 
                WHERE CartId = " + customerId;
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
                reader.Close();
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
                reader.Close();
            }
            return product;
        }

        public static bool IsActiveCartId(int customerId, int productId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT COUNT(*) FROM CartDetails
                    WHERE CartId = " + customerId + " AND ProductId = " + productId;
                SqlCommand cmd = new SqlCommand(sql, conn);
                int count = (int)cmd.ExecuteScalar();
                conn.Close();
                return (count >= 1);
            }
        }

        public static void CreateCart(int CustomerId, int ProductId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO CartDetails 
                            VALUES ( " + CustomerId + ", " + ProductId + ", 1, GETDATE())";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static void AddToCart(int ProductId, int CustomerId)
        {
            if (IsActiveCartId(CustomerId, ProductId) == false)
                CreateCart(CustomerId, ProductId);
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"SELECT Quantity FROM CartDetails 
                                        WHERE CartId = '" + CustomerId + "' AND ProductId = " + ProductId;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    int quantity = (int)cmd.ExecuteScalar();
                    sql = @"UPDATE CartDetails SET Quantity = " + (quantity + 1) + ", LastUpdateDate = GETDATE() " +
                                 "WHERE CartId = '" + CustomerId + "' AND ProductId = " + ProductId;
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public static int GetCartQuantity(int customerId)
        {
            List<CartDetail> cart = CartData.GetCart(customerId);
            int quantity = 0;

            foreach (var cartItem in cart)
            {
                quantity += cartItem.Quantity;
            }
            return quantity;
        }


        public static void ClearCart(int CustomerId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"Delete from CartDetails where CartId = " + CustomerId;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        public static void UpdateQty(int CartID, int ProductID, int Qty)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"UPDATE CartDetails SET Quantity = "+ Qty + ", LastUpdateDate = GetDate() WHERE CartID = " + CartID+" and ProductID = "+ProductID+";";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void RemoveProduct(int CartID, int ProductID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"DELETE FROM CartDetails WHERE CartID = " + CartID + " and ProductID = " + ProductID + ";";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


    }
}