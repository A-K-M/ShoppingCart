
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ShoppingCart.Models;
using ShoppingCart.Util;

namespace ShoppingCart.Database
{
    public class PurchaseData
    {
        //public static List<Purchase> GetPurchaseBySessionId(string sessionId)
        //{
        //    List<Purchase> Purchase = new List<Purchase>();
        //    using (SqlConnection conn = new SqlConnection(Data.connectionString))
        //    {
        //        conn.Open();

        //        string sql = @"SELECT OrderDate, Customers.CustomerId, PurchaseId from Purchases,Customers
        //                     where Customers.CustomerId=Purchases.CustomerId 
        //                     AND sessionId= '" + sessionId + "'";
        //        SqlCommand cmd = new SqlCommand(sql, conn);

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Purchase _Purchase = new Purchase()
        //            {
        //                PurchaseId = (int)reader["PurchaseId"],
        //                CustomerId = (int)reader["CustomerId"],

        //                OrderDate = (DateTime)reader["OrderDate"]
        //            };

        //            Purchase.Add(_Purchase);
        //        }
        //    }
        //    return Purchase;
        //}

        public static List<Purchase> GetPurchaseDetailsByCustomerId(int customerId)
        {
            List<Purchase> purchases = new List<Purchase>();

            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string sql = @"SELECT * FROM Purchases WHERE CustomerId=@customerId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Purchase purchase = new Purchase()
                    {
                        PurchaseId = (int)reader["PurchaseId"],
                        CustomerId = (int)reader["CustomerId"],
                        OrderDate = (DateTime)reader["OrderDate"]
                    };
                    purchases.Add(purchase);
                }
                reader.Close();

                foreach (Purchase purchase in purchases)
                {
                    int purchaseId = purchase.PurchaseId;
                    List<PurchaseDetails> purchaseDetails = new List<PurchaseDetails>();
                    sql = @"SELECT DISTINCT P.ProductId, ProductName, ProductDescription, UnitPrice, Image, PurchaseId, PD.ProductId
                                FROM Products P, PurchaseDetails PD 
                                WHERE P.ProductId=pd.ProductId and PD.PurchaseId=@purchaseId";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@purchaseId", purchase.PurchaseId);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PurchaseDetails det = new PurchaseDetails
                        {
                            Product = new Product()
                            {
                                ImagePath = (string)reader["Image"],
                                ProductDescription = (string)reader["ProductDescription"],
                                ProductId = (int)reader["ProductId"],
                                ProductName = (string)reader["ProductName"]
                            },
                            //ActCodes = ShopUtil.GetActivationSelectList((string)reader["ActivationCode"]),
                            //ActCodes = ShopUtil.GetActivationList((string)reader["ActivationCode"]),
                            ProductId = (int)reader["ProductId"],
                            PurchaseId = (int)reader["PurchaseId"],
                        };
                        purchaseDetails.Add(det);
                    }
                    purchase.PurchaseDetails = purchaseDetails;
                    reader.Close();

                    foreach (PurchaseDetails purchaseDetail in purchaseDetails)
                    {
                        purchaseDetail.ActivationCodes = new List<string>();
                        int purchaseId2 = purchaseDetail.PurchaseId;
                        int productId2 = purchaseDetail.ProductId;

                        sql = @"SELECT ActivationCode 
	                                FROM PurchaseDetails
	                                WHERE PurchaseId = " + purchaseId2 + " AND ProductId = " + productId2;
                        cmd = new SqlCommand(sql, conn);
                        reader = cmd.ExecuteReader();
                        if (reader == null)
                        {
                            return null;
                        }
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                purchaseDetail.ActivationCodes.Add((string)reader["ActivationCode"]);
                            }
                        }
                        reader.Close();
                    }
                }
            }
            return purchases;
        }


        public static void CreateNewPurchase(int customerId)
        {
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();
                int p_count = CountPurchases();
                string sql = @"insert into purchases(PurchaseId, CustomerId, OrderDate) values ("+ (p_count+1 )+ ","+ customerId + ",GetDate())";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }


        public static void InsertNewPurchses(int productId)
        {
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();
                int p_count = CountPurchases();
                string ac_code = ShopUtil.GenerateActCode();
                string sql = @"insert into purchasedetails(PurchaseId, ProductId, ActivationCode) values (" + (p_count) + "," + productId + ",'"+ ac_code + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public static int CountPurchases()
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string sql = @"select count(PurchaseId) from Purchases;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                count = (int)cmd.ExecuteScalar();
            }
            return count;
        }
    }
}
