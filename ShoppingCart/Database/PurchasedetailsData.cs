﻿
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ShoppingCart.Models;

namespace ShoppingCart.Database
{
    public class PurchaseDetailsData
    {
        public static List<PurchaseDetails> GetPurchaseDetailsBySessionId(string sessionId)
        {
            List<PurchaseDetails> PurchaseDetails = new List<PurchaseDetails>();
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string sql = @"SELECT PurchaseDetails.PurchaseId,PurchaseDetails.ProductId,ActivationCode from PurchaseDetails,Purchases,Customers
                             where Customers.CustomerId=Purchases.CustomerId AND Purchases.PurchaseID=PurchaseDetails.PurchaseId
                             AND sessionId= '" + sessionId + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PurchaseDetails _PurchaseDetails = new PurchaseDetails()
                    {
                        PurchaseId = (int)reader["PurchaseId"],
                        ProductId = (int)reader["ProductID"],
                        ActivationCode = (string)reader["ActivationCode"]
                    };

                    PurchaseDetails.Add(_PurchaseDetails);
                }
            }
            return PurchaseDetails;
        }

        public static Purchase GetPurchaseByPurchaseId(int PurchaseId)
        {
            Purchase purchase = new Purchase();
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string sql = @"SELECT OrderDate, CustomerId, PurchaseId FROM Purchases
                             WHERE PurchaseId = " + PurchaseId;
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    purchase = new Purchase()
                    {
                        PurchaseId = (int)reader["PurchaseId"],
                        CustomerId = (int)reader["CustomerId"],
                        OrderDate = (DateTime)reader["OrderDate"]
                    };
                }
            }
            return purchase;
        }

        public static Product GetProductByProductId(int ProductId)
        {
            Product product = new Product();
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string sql = @"SELECT ProductId, ProductName, ProductDescription, UnitPrice, Image
                            FROM Products WHERE ProductId =" + ProductId;
                SqlCommand cmd = new SqlCommand(sql, conn);
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


        public static void GenerateActivationCode()
        {

        }
    }  
}