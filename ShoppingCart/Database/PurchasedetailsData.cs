
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

                string sql = @"SELECT PurchaseId,ProductId,ActivationCode from PurchaseDetails,Purchases,Customers
                             where Customers.CustomerId=Purchases.CustomerId AND Purchases.PurchaseID=PurchaseDetails.PurchaseId
                             AND sessionId= '" + sessionId +"'";
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
        
}