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

                string sql = @"SELECT ProductId, ProductName, ProductDescription, UnitPrice, ImagePath
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
                            ImagePath = (string)reader["ImagePath"]
                        });
                    }
                }
            }
            return products_list;
        }

        public List<Product> GetSearchProducts(string searchObj)
        {
            List<Product> Searched_products_list = new List<Product>();
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();
                string sql = @"SELECT * FROM Products where ProductName like '%" + searchObj + "%' " +
                    "or ProductDescription like '%" + searchObj + "%'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Searched_products_list.Add(new Product()
                        {
                            ProductId = (int)reader["ProductId"],
                            ProductName = (string)reader["ProductName"],
                            ProductDescription = (string)reader["ProductDescription"],
                            UnitPrice = (decimal)reader["UnitPrice"],
                            ImagePath = (string)reader["ImagePath"]
                        });
                    }
                }
                return Searched_products_list;
            }
        }
    }
}