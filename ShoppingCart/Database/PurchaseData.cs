using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ShoppingCart.Database
{
    public class PurchaseData : Database
    {
        public static List<Purchase> GetPurchasesByCustomerId(int customerID)
        {
            List<Purchase> Purchases = new List<Purchase>();
            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string sql = @"SELECT PurchaseID from Purchases where CustomerID=" + CustomerID;
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Purchase _Purchase = new Purchase()
                    {
                        PurchaseId = (int)reader["PurchaseId"]
                    };

                    Purchase.Add(_Purchase);
                }



            }
            return Purchase;
        }
    }
}