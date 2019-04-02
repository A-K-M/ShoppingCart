using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Purchases
    {
        public int PurchaseId { get; set; }
        public int CustomerId { get; set; }
        public string OrderDate { get; set; }
    }
}