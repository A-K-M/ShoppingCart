
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
        public static List<PurchaseDetail> GetPurchaseDetailsByPurchaseId(int PurchaseID)
        {
            List<PurchaseDetail> PurchaseDetails = new List<PurchaseDetail>();
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string sql = @"SELECT PurchaseID,ProductID,ActivationCode from PurchaseDetails where PurchaseID=" + PurchaseID;
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PurchaseDetail _PurchaseDetails = new PurchaseDetail()
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
}