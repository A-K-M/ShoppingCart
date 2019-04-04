using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<PurchaseDetails> PurchaseDetails { get; set; }
    }
}