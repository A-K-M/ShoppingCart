
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Database
{
    public class PurchaseDetailsData
    {
        public static List<PurchaseDetails> GetPurchaseDetailsByPurchaseId(int PurchaseID)
        {
            List<PurchaseDetails> PurchaseDetails = new List<PurchaseDetails>();
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string sql = @"SELECT PurchaseID,ProductID,ActivationCode from PurchaseDetails where PurchaseID=" + PurchaseID;
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PurchaseDetails _PurchaseDetails = new PurchaseDetails()
                    {
                        PurchaseId = (int)reader["PurchaseId"],
                        ProductID = (int)reader["ProductID"],
                        ActivationCode = (string)reader["ActivationCode"]
                    };

                    PurchaseDetails.Add(_PurchaseDetails);
                }
            }
            return PurchaseDetails;
        }
    }
}